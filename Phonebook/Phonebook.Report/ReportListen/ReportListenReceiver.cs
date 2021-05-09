using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Phonebook.EventBus;
using Phonebook.Report.DAL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Report.ReportListen
{
    public class ReportListenReceiver : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _queueName = "report_result";
        private readonly IUnitOfWork _unitOfWork;
        private IConnection _connection;
        private IModel _channel;

        public ReportListenReceiver(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _hostname = appSettings.Value.RabbitMqIp;

            InitializeRabbitMqListener();
            _unitOfWork = unitOfWork;
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = "ovommrzd",
                VirtualHost= "ovommrzd",
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
                var reportResultMessage = JsonConvert.DeserializeObject<ReportResultMessage>(content);

                HandleMessage(reportResultMessage);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(ReportResultMessage reportResultMessage)
        {
            var report = _unitOfWork.ReportRepository.GetByID(reportResultMessage.ReportId);
            if (report != null)
            {
                report.ReportDetail = new Entity.ReportDetail()
                {
                    CreateDate = DateTime.Now,
                    PersonCount = reportResultMessage.PersonCount,
                    PhoneCount = reportResultMessage.PhoneCount
                };

                report.ReportStatus = Entity.Enums.EnumReportStatus.Completed;
            }

            _unitOfWork.ReportRepository.Update(report);
            _unitOfWork.Save();
        }
    }
}
