using System.Collections;

namespace CQRSTutorial.Cafe.Common
{
    public interface ICommandHander<TCommand>
    {
        IEnumerable Handle(TCommand c);
    }
}
