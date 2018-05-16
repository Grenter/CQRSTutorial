using System;
using System.Threading.Tasks;
using MassTransit;

namespace CQRSTutorial.Cafe.Messaging
{
    public class RabbitMqEndpointProvider : ISendEndPointProvider
    {
        private readonly IBusControl _busControl;
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;

        public RabbitMqEndpointProvider(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;

            _busControl = Bus.Factory.CreateUsingRabbitMq(rmqf =>
            {
                rmqf.Host(new Uri(rabbitMqConfiguration.Uri), h =>
                {
                    h.Username(rabbitMqConfiguration.Username);
                    h.Password(rabbitMqConfiguration.Password);
                });
            });
        }

        public async Task<ISendEndpoint> GetEndpoint(string queueName)
        {
            var uri = new Uri($"{_rabbitMqConfiguration.Uri}/{queueName}");
            return await _busControl.GetSendEndpoint(uri);
        }
    }
}
