using CQRSTutorial.Events;

namespace CQRSTutorial.Core
{
    public interface IApplyEvent<in TEvent>
        where TEvent : IDomainEvent
    {
        void Apply(TEvent domainEvent);
    }
}