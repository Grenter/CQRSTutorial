using System.Threading.Tasks;
using MassTransit;

namespace CQRSTutorial.Cafe.Web.Messaging
{
    public interface ISendEndPointProvider
    {
        Task<ISendEndpoint> GetEndpoint(string queueName);
    }
}