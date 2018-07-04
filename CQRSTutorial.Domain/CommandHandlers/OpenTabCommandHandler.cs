using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Messaging;
using System.Linq;

namespace CQRSTutorial.Domain.CommandHandlers
{
    public class OpenTabCommandHandler : ICommandHandler<OpenTab>
    {
        private readonly IMessageBus _messageBus;

        public OpenTabCommandHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Handle(OpenTab command)
        {
            var tab = new TabAggregate(command.AggregateId, command.TableNumber, command.WaiterName);

            tab.RaiseLastEvent(@event => _messageBus.RaiseEvent(@event));

        }
    }
}
