using CQRSTutorial.Commands;
using CQRSTutorial.Core;
using CQRSTutorial.Events;
using CQRSTutorial.EventStore;
using System;

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
            var tabOpened = new TabOpened
            {
                Id = Guid.NewGuid(),
                AggregateId = command.AggregateId,
                Balance = 0.0m,
                TableNumber = command.TableNumber,
                WaiterName = command.WaiterName
            };

            var tab = new TabAggregate(tabOpened);

            _repository.Add(tabOpened);

            return tabOpened;
        }
    }
}
