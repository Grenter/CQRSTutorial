namespace CQRSTutorial.Cafe.Messaging
{
    public interface IRabbitMqConfiguration
    {
        string Uri { get; }
        string Username { get; }
        string Password { get; }
    }

    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string Uri => "rabbitmq://localhost";
        public string Username => "guest";
        public string Password => "guest";
    }
}