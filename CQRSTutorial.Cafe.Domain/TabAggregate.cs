using CQRSTutorial.Cafe.Domain.Commands;
using CQRSTutorial.Cafe.Events;
using System.Collections;
using System.Linq;
using CQRSTutorial.Cafe.Common;

namespace CQRSTutorial.Cafe.Domain
{
    public class TabAggregate : Aggregate,
        ICommandHander<OpenTabCommand>,
        ICommandHander<PlaceOrderCommand>
    {
        private bool _tabOpen;

        public IEnumerable Handle(OpenTabCommand c)
        {
            yield return new TabOpened
            {
                Id = c.Id,
                TableNumber = c.TableNumber,
                Waiter = c.Waiter
            };
        }

        public IEnumerable Handle(PlaceOrderCommand c)
        {
            throw new TabNotOpen();
        }
    }
}
