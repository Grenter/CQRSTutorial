using System;
using System.Threading.Tasks;
using CQRSTutorial.Cafe.Commands;
using MassTransit;

namespace CQRSTutorial.Cafe.Web.Messaging
{
    public class CommandSender : ICommandSender
    {
        private IEndPointProvider _endPointProvider;

        public CommandSender(IEndPointProvider endPointProvider)
        {
            _endPointProvider = endPointProvider;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var sendEndpoint = await _endPointProvider.GetEndpoint("cafe.waiter.command.service");
            await sendEndpoint.Send(command);
        }
    }

    public interface ICommandSender
    {
        Task Send<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
