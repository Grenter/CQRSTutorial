using CQRSTutorial.Core.Events;

namespace CQRSTutorial.EventStore
{
    public interface IEventRepository
    {
        void Add(IDomainEvent domainEvent);
    }
}