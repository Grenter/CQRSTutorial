namespace CQRSTutorial.Cafe.Domain
{
    public interface ICommandDispatcher
    {
        void DispatchCommand<TCommand>(TCommand c);
    }
}