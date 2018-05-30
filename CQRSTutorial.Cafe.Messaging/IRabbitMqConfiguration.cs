namespace CQRSTutorial.Cafe.Messaging
{
    public interface IRabbitMqConfiguration
    {
        string Uri { get; }
        string Username { get; }
        string Password { get; }
    }
}