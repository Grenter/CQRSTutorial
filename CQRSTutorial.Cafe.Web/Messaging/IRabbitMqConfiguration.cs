namespace CQRSTutorial.Cafe.Web.Messaging
{
    public interface IRabbitMqConfiguration
    {
        string Uri { get; }
        string Username { get; }
        string Password { get; }
    }
}