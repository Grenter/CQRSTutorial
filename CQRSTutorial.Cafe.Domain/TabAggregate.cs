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
        ICommandHander<CloseTabCommand>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<DrinksServed>,
        IApplyEvent<FoodOrdered>,
        IApplyEvent<FoodServed>
    {
        private bool _tabOpen;
        private List<OrderedItem> _outstandingDrinks = new List<OrderedItem>();
        private List<OrderedItem> _outstandingFood = new List<OrderedItem>();
        private List<OrderedItem> _preparedFood = new List<OrderedItem>();
        private decimal _serveredItemsValue = 0M;

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
            if (!AreFoodOutstanding(c.MenuNumbers))
                throw new FoodNotOutstanding();

            yield return new FoodServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public IEnumerable Handle(CloseTabCommand c)
        {
            yield return new TabClosed
            {
                Id = c.Id,
                AmmountPaid = c.AmmountPaid,
                OrderValue = _serveredItemsValue,
                TipValue = c.AmmountPaid - _serveredItemsValue
            };
        }

        public void Apply(TabOpened e)
        {
            _tabOpen = true;
        }

        public void Apply(DrinksOrdered e)
        {
            _outstandingDrinks.AddRange(e.Items);
        }

        public void Apply(DrinksServed e)
        {
            foreach (var menuNumber in e.MenuNumbers)
            {
                var item = _outstandingDrinks.First(d => d.MenuNumber == menuNumber);
                _outstandingDrinks.Remove(item);
                _serveredItemsValue += item.Price;
            }
        }

        public void Apply(FoodOrdered e)
        {
            _outstandingFood.AddRange(e.Items);
        }

        public void Apply(FoodServed e)
        {
            foreach (var menuNumber in e.MenuNumbers)
            {
                var item = _outstandingFood.First(f => f.MenuNumber == menuNumber);
                _outstandingFood.Remove(item);
                _serveredItemsValue += item.Price;
            }
        }

        private bool AreDrinksOutstanding(IEnumerable<int> menuNumbers)
        {
            var curOutstanding = new List<OrderedItem>(_outstandingDrinks);

            foreach (var menuNumber in menuNumbers)
            {
                var item = curOutstanding.FirstOrDefault(i => i.MenuNumber == menuNumber);
                if (curOutstanding.Contains(item))
                    curOutstanding.Remove(item);
                else
                    return false;
            }

            return true;
        }

        private bool AreFoodOutstanding(IEnumerable<int> menuNumbers)
        {
            var curOutstanding = new List<OrderedItem>(_outstandingFood);

            foreach (var menuNumber in menuNumbers)
            {
                var item = curOutstanding.FirstOrDefault(i => i.MenuNumber == menuNumber);
                if (curOutstanding.Contains(item))
                    curOutstanding.Remove(item);
                else
                    return false;
            }

            return true;
        }
    }
}