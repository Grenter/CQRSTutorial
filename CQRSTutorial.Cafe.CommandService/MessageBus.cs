using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Monitoring.Performance;

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
