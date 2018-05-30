using System.Collections;

namespace CQRSTutorial.Cafe.Common
{
    public interface ICommandHander<TCommand> : ICommandHandler
    {
        IEnumerable Handle(TCommand command);
    }

    public interface ICommandHandler
    {
        bool CanHandle(ICommand command);
    }
}
