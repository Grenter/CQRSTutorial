using CQRSTutorial.Cafe.Commands;
using System.Threading.Tasks;

namespace CQRSTutorial.Cafe.Web.Messaging
{
    public class CommandSender : ICommandSender
    {
        private readonly ISendEndPointProvider _sendEndPointProvider;
        private ISendEndpointConfiguration _sendEndpointConfiguration;

        public CommandSender(ISendEndPointProvider sendEndPointProvider, ISendEndpointConfiguration sendEndpointConfiguration)
        {
            _sendEndPointProvider = sendEndPointProvider;
            _sendEndpointConfiguration = sendEndpointConfiguration;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var sendEndpoint = await _sendEndPointProvider.GetEndpoint(_sendEndpointConfiguration.Queue);
            await sendEndpoint.Send(command);
        }
    }
}
