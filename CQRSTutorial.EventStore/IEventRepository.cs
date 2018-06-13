using CQRSTutorial.Core.Events;

namespace CQRSTutorial.EventStore
{
    public interface IEventRepository
    {
        void Add<TDomainEvent>(IDomainEvent domainEvent) where TDomainEvent : class, IDomainEvent;
    }
}