using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Phonebook.EventBus;
using Phonebook.Main.DAL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Main.ReportGenerate
{
    public class ReportGenerateReceiver : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _queueName = "report_create";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        private IConnection _connection;
        private IModel _channel;

        public ReportGenerateReceiver(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _hostname = appSettings.Value.RabbitMqIp;

            InitializeRabbitMqListener();
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = "ovommrzd",
                VirtualHost = "ovommrzd",
                Password = "PN_LcxVCW14Bdkk4fu8Ey0sgPc3RPpQC"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var reportGenerateMessage = JsonConvert.DeserializeObject<ReportGenerateMessage>(content);

                HandleMessage(reportGenerateMessage);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(ReportGenerateMessage reportGenerateMessage)
        {
            double roundedLatitude = Math.Round(reportGenerateMessage.Latitude);
            double roundedLongitude = Math.Round(reportGenerateMessage.Longitude);
            string roundedLocation = string.Join(",", roundedLatitude.ToString(), roundedLongitude.ToString());

            int personCount = _unitOfWork.ContactInfoRepository.Get().Where(t => t.ContactType == Entity.Enums.EnumContactType.GeoLocation).Where(t => t.GetRoundedLocation().GetValueOrDefault().Item1 == roundedLatitude).Where(t => t.GetRoundedLocation().GetValueOrDefault().Item2 == roundedLongitude).Select(t => t.Person).Count();
            var phoneCount = _unitOfWork.ContactInfoRepository.Get().Where(t => t.ContactType == Entity.Enums.EnumContactType.GeoLocation).Where(t => t.GetRoundedLocation().GetValueOrDefault().Item1 == roundedLatitude).Where(t => t.GetRoundedLocation().GetValueOrDefault().Item2 == roundedLongitude).Where(t => t.Person.ContactInfos.Any(c => c.ContactType == Entity.Enums.EnumContactType.PhoneType)).Select(t => t.Person.ContactInfos.Count(t => t.ContactType == Entity.Enums.EnumContactType.PhoneType)).Sum();

            var reportResultMessage = new
            {
                PersonCount = personCount,
                PhoneCount = phoneCount,
                ReportId = reportGenerateMessage.ReportId
            };

            _eventBus.PushMessage("report_result", JsonConvert.SerializeObject(reportResultMessage));
        }
    }
}
