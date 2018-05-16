using System.Threading.Tasks;
using MassTransit;

namespace CQRSTutorial.Cafe.Messaging
{
    public interface ISendEndPointProvider
    {
        Task<ISendEndpoint> GetEndpoint(string queueName);
    }
}