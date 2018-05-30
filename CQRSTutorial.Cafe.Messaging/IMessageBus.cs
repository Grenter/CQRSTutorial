using MassTransit;

namespace CQRSTutorial.Cafe.Messaging
{
    public interface IMessageBus
    {
        IBusControl Create();
    }
}