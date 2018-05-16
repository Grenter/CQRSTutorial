using MassTransit;
using System;
using CQRSTutorial.Cafe.Messaging;

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
}
