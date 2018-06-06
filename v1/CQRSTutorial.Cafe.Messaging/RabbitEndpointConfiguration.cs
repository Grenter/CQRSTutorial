namespace CQRSTutorial.Cafe.Messaging
{
    public class RabbitEndpointConfiguration : ISendEndpointConfiguration
    {
        public string Queue => "cafe.waiter.command.service";
    }
}