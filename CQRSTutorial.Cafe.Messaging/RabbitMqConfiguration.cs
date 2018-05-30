namespace CQRSTutorial.Cafe.Messaging
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string Uri => "rabbitmq://localhost";
        public string Username => "guest";
        public string Password => "guest";
    }
}