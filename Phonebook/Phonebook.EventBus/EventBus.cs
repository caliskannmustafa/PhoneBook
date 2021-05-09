using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.EventBus
{
    public class EventBus : IEventBus
    {
        private ConnectionFactory _connectionFactory;
        public EventBus(IOptions<AppSettings> appSettings)
        {
            _connectionFactory = new ConnectionFactory() { HostName = appSettings.Value.RabbitMqIp, UserName = "admin", Password = "admin" };
        }

        public void PushMessage(string queueName, string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = UTF8Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }
    }
}
