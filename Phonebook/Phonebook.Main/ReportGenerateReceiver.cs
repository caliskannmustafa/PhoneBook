using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Phonebook.EventBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Main
{
    public class ReportGenerateReceiver : BackgroundService
    {
        private readonly string _hostname;
        private readonly string _queueName= "report_create";
        private IConnection _connection;
        private IModel _channel;

        public ReportGenerateReceiver(IOptions<AppSettings> appSettings)
        {
            _hostname = appSettings.Value.RabbitMqIp;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = "admin",
                Password = "admin"
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
                var reportId = JsonConvert.DeserializeObject<string>(content);

                HandleMessage(reportId);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName,true, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(string reportId) 
        {
            Console.WriteLine(reportId);
        }
    }
}
