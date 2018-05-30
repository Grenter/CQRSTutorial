namespace CQRSTutorial.Cafe.Messaging
{
    public interface ISendEndpointConfiguration
    {
        string Queue { get; }
    }

    public class RabbitEndpointConfiguration : ISendEndpointConfiguration
    {
        public string Queue => "cafe.waiter.command.service";
    }
}