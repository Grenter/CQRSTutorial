using CQRSTutorial.Cafe.Domain.Commands;
using CQRSTutorial.Cafe.Events;
using System.Collections;
using System.Linq;
using CQRSTutorial.Cafe.Common;

namespace CQRSTutorial.Cafe.Domain
{
    public class TabAggregate : Aggregate,
        ICommandHander<OpenTabCommand>,
        ICommandHander<PlaceOrderCommand>,
        IApplyEvent<TabOpened>
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
            if (!_tabOpen) throw new TabNotOpen();

            var drink = c.Items.Where(i => i.IsDrink).ToList();
            if (drink.Any())
            {
                yield return new DrinksOrdered
                {
                    Id = c.Id,
                    Items = drink
                };
            }

            var food = c.Items.Where(i => !i.IsDrink).ToList();
            if (food.Any())
            {
                yield return new FoodOrdered
                {
                    Id = c.Id,
                    Items = food
                };
            }
        }

        public void Apply(TabOpened e)
        {
            _tabOpen = true;
        }
    }
}
