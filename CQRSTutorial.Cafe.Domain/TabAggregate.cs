using CQRSTutorial.Cafe.Domain.Commands;
using CQRSTutorial.Cafe.Events;
using System.Collections;

namespace CQRSTutorial.Cafe.Domain
{
    public class TabAggregate : Aggregate,
        ICommandHander<OpenTabCommand>,
    {
        public IEnumerable Handle(OpenTabCommand c)
        {
            yield return new TabOpened
            {
                Id = c.Id,
                TableNumber = c.TableNumber,
                Waiter = c.Waiter
            };
        }
    }
}
