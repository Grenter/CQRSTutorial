using CQRSTutorial.Cafe.Common;
using System.Threading.Tasks;

namespace CQRSTutorial.Cafe.Messaging
{
    public class CommandSender : ICommandSender
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private ISendEndpointConfiguration _sendEndpointConfiguration;

        public CommandSender(ISendEndpointProvider sendEndpointProvider, ISendEndpointConfiguration sendEndpointConfiguration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _sendEndpointConfiguration = sendEndpointConfiguration;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var sendEndpoint = await _sendEndpointProvider.GetEndpoint(_sendEndpointConfiguration.Queue);
            await sendEndpoint.Send(command);
        }
    }
}
