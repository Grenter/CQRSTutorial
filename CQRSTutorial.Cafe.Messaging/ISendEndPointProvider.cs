using System.Threading.Tasks;
using MassTransit;

namespace CQRSTutorial.Cafe.Messaging
{
    public interface ISendEndpointProvider
    {
        Task<ISendEndpoint> GetEndpoint(string queueName);
    }
}