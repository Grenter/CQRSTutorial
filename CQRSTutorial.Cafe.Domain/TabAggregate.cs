using System;
using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Common;
using CQRSTutorial.Cafe.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CQRSTutorial.Cafe.Domain
{
    public class TabAggregate : Aggregate,
        ICommandHander<OpenTabCommand>,
        ICommandHander<PlaceOrderCommand>,
        ICommandHander<ServeDrinksCommand>,
        ICommandHander<ServeFoodCommand>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<DrinksServed>,
        IApplyEvent<FoodOrdered>
    {
        private bool _tabOpen;
        private List<int> _outstandingDrinks = new List<int>();

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

        public IEnumerable Handle(ServeDrinksCommand c)
        {
            if (!AreDrinksOutstanding(c.MenuNumbers))
                throw new DrinksNotOutstanding();

            yield return new DrinksServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public IEnumerable Handle(ServeFoodCommand c)
        {
            yield return new FoodServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public void Apply(TabOpened e)
        {
            _tabOpen = true;
        }

        public void Apply(DrinksOrdered e)
        {
            _outstandingDrinks.AddRange(e.Items.Select(i => i.MenuNumber));
        }

        public void Apply(DrinksServed e)
        {
            foreach (var menuNumber in e.MenuNumbers)
            {
                _outstandingDrinks.Remove(menuNumber);
            }
        }

        public void Apply(FoodOrdered e)
        {
            
        }

        private bool AreDrinksOutstanding(IEnumerable<int> menuNumbers)
        {
            var curOutstanding = new List<int>(_outstandingDrinks);

            foreach (var menuNumber in menuNumbers)
            {
                if (curOutstanding.Contains(menuNumber))
                    curOutstanding.Remove(menuNumber);
                else
                    return false;
            }

            return true;
        }
    }
}