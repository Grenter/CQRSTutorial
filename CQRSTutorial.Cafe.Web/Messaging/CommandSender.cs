using System;
using System.Threading.Tasks;
using CQRSTutorial.Cafe.Commands;
using MassTransit;

namespace CQRSTutorial.Cafe.Web.Messaging
{
    public class CommandSender : ICommandSender
    {
        private ISendEndPointProvider _sendEndPointProvider;

        public CommandSender(ISendEndPointProvider sendEndPointProvider)
        {
            _sendEndPointProvider = sendEndPointProvider;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var sendEndpoint = await _sendEndPointProvider.GetEndpoint("cafe.waiter.command.service");
            await sendEndpoint.Send(command);
        }
    }
}
