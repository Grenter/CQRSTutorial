using MassTransit;
using System;

namespace CQRSTutorial.Cafe.CommandService
{
    public class MessageBus : IMessageBus
    {
        public IBusControl Create()
        {
            return Bus.Factory.CreateUsingRabbitMq(rmqf =>
            {
                rmqf.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }
    }

    public interface IMessageBus
    {
        IBusControl Create();
    }
}
