using MassTransit;
using System;

namespace CQRSTutorial.Cafe.Messaging
{
    public interface IMessageBus
    {
        IBusControl Create();
    }

    public class RabbitMessageBus : IMessageBus
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;

        public RabbitMessageBus(IRabbitMqConfiguration rabbitMqConfiguration)
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