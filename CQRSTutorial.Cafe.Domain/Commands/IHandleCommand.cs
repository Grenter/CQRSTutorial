using System.Collections;

namespace CQRSTutorial.Cafe.Domain.Commands
{
    public interface IHandleCommand<TCommand>
    {
        IEnumerable Handle(TCommand c);
    }
}
