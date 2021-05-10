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

namespace Phonebook.Main.DataGenerate
{
    public class DataGenerateReceiver : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _queueName = "data_generate";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        private readonly IRandomDataGenerator _randomDataGenerator;
        private IConnection _connection;
        private IModel _channel;

        public DataGenerateReceiver(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IEventBus eventBus, IRandomDataGenerator randomDataGenerator)
        {
            _hostname = appSettings.Value.RabbitMqIp;

            InitializeRabbitMqListener();
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
            _randomDataGenerator = randomDataGenerator;
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
                var dataGenerateMessage = JsonConvert.DeserializeObject<int>(content);

                HandleMessage(dataGenerateMessage);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(int dataCount)
        {
            _randomDataGenerator.Generate(dataCount);
        }
    }
}
