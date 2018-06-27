namespace CQRSTutorial.Core
{
    public interface IApplyEvent<in TEvent>
        where TEvent : IDomainEvent
    {
        void When(TEvent @event);
    }
}