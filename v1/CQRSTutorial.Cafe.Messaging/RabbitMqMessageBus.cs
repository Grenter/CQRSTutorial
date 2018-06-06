using System;
using MassTransit;

namespace CQRSTutorial.Cafe.Messaging
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;

        public RabbitMqMessageBus(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        public IBusControl Create()
        {
            return Bus.Factory.CreateUsingRabbitMq(rmqf =>
            {
                rmqf.Host(new Uri(_rabbitMqConfiguration.Uri), h =>
                {
                    h.Username(_rabbitMqConfiguration.Username);
                    h.Password(_rabbitMqConfiguration.Password);
                });
            });
        }
    }
}