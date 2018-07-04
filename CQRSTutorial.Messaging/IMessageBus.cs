using CQRSTutorial.Core;

namespace CQRSTutorial.Messaging
{
    public interface IMessageBus
    {
        void RegisterEventHandler<T>(T eventHandler) where T : IEventListener;

        void RaiseEvent<T>(T domainEvent) where T : IDomainEvent;
    }
}