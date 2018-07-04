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
            var raisedEvent = tab.GetDomainEvents().Last();

            _messageBus.RaiseEvent(raisedEvent);
        }
    }
}
