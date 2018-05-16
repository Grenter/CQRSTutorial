using System;
using MassTransit;
using System.Threading.Tasks;

namespace CQRSTutorial.Cafe.Web.Messaging
{
    public interface IEndPointProvider
    {
        Task<ISendEndpoint> GetEndpoint(string queueName);
    }

    public class RabbitMqEndpointProvider : IEndPointProvider
    {
        private const string BaseUri = "rabbitmq://localhost";

        private IBusControl _busControl;

        public RabbitMqEndpointProvider()
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(rmqf =>
            {
                rmqf.Host(new Uri(BaseUri), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }

        public async Task<ISendEndpoint> GetEndpoint(string queueName)
        {
            var uri = new Uri($"{BaseUri}/{queueName}");
            return await _busControl.GetSendEndpoint(uri);
        }
    }


}
