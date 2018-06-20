namespace CQRSTutorial.Core
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        IDomainEvent Handle(TCommand command);
    }
}