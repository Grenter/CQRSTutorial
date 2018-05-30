using System.Collections;

namespace CQRSTutorial.Cafe.Common
{
    public interface ICommandHandler<TCommand>
    {
        IEnumerable Handle(TCommand command);
    }
}
