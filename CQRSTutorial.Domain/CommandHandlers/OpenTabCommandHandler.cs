using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.EventStore;
using System.Linq;

namespace CQRSTutorial.Domain.CommandHandlers
{
    public class OpenTabCommandHandler : ICommandHandler<OpenTab>
    {
        private readonly IEventRepository _repository;

        public OpenTabCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public IDomainEvent Handle(OpenTab command)
        {
            var tab = new TabAggregate(command.AggregateId, command.TableNumber, command.WaiterName);
            var raisedEvent = tab.GetDomainEvents().Last();

            _repository.Add(raisedEvent); // Temp until Event Listeners and bus added. 

            return raisedEvent;
        }
    }
}
