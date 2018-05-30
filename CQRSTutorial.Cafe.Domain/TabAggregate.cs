using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Common;
using CQRSTutorial.Cafe.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CQRSTutorial.Cafe.Domain
{
    public class TabAggregate : Aggregate,
        ICommandHandler<OpenTabCommand>,
        ICommandHandler<PlaceOrderCommand>,
        ICommandHandler<ServeDrinksCommand>,
        ICommandHandler<PrepareFoodCommand>,
        ICommandHandler<ServeFoodCommand>,
        ICommandHandler<CloseTabCommand>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>,
        IApplyEvent<DrinksServed>,
        IApplyEvent<FoodOrdered>,
        IApplyEvent<FoodPrepared>,
        IApplyEvent<FoodServed>,
        IApplyEvent<TabClosed>
    {
        private bool _tabOpen;
        private readonly List<OrderedItem> _outstandingDrinks = new List<OrderedItem>();
        private readonly List<OrderedItem> _outstandingFood = new List<OrderedItem>();
        private readonly List<OrderedItem> _preparedFood = new List<OrderedItem>();
        private decimal _serveredItemsValue;

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
            if (!AreItemsOutstanding(c.MenuNumbers, _outstandingDrinks))
                throw new DrinksNotOutstanding();

            yield return new DrinksServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public IEnumerable Handle(PrepareFoodCommand c)
        {
            if (!AreItemsOutstanding(c.MenuNumbers, _outstandingFood))
                throw new FoodNotOutstanding();

            yield return new FoodPrepared
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public IEnumerable Handle(ServeFoodCommand c)
        {
            if (!AreItemsOutstanding(c.MenuNumbers, _preparedFood))
                throw new FoodNotPrepared();

            yield return new FoodServed
            {
                Id = c.Id,
                MenuNumbers = c.MenuNumbers
            };
        }

        public IEnumerable Handle(CloseTabCommand c)
        {
            if (c.AmountPaid < _serveredItemsValue)
                throw new NotEnoughPaid();

            if (!_tabOpen)
                throw new TabNotOpen();

            if (_outstandingDrinks.Any() || _outstandingFood.Any() || _preparedFood.Any())
                throw new TabHasUnservedItems();

            yield return new TabClosed
            {
                Id = c.Id,
                AmountPaid = c.AmountPaid,
                OrderValue = _serveredItemsValue,
                TipValue = c.AmountPaid - _serveredItemsValue
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
                var item = _outstandingDrinks.FirstOrDefault(d => d.MenuNumber == menuNumber);
                _outstandingDrinks.Remove(item);
                _serveredItemsValue += item.Price;
            }
        }

        public void Apply(FoodOrdered e)
        {
            _outstandingFood.AddRange(e.Items);
        }

        public void Apply(FoodPrepared e)
        {
            foreach (var menuNumber in e.MenuNumbers)
            {
                var item = _outstandingFood.FirstOrDefault(f => f.MenuNumber == menuNumber);
                _outstandingFood.Remove(item);
                _preparedFood.Add(item);
            }
        }

        public void Apply(FoodServed e)
        {
            foreach (var menuNumber in e.MenuNumbers)
            {
                var item = _preparedFood.FirstOrDefault(f => f.MenuNumber == menuNumber);
                _preparedFood.Remove(item);
                _serveredItemsValue += item.Price;
            }
        }

        public void Apply(TabClosed e)
        {
            _tabOpen = false;
        }

        private static bool AreItemsOutstanding(IEnumerable<int> menuNumbers, IEnumerable<OrderedItem> oustandingItems)
        {
            var currentItems = new List<int>(oustandingItems.Select(i => i.MenuNumber));
            foreach (var menuNumber in menuNumbers)
            {
                if (currentItems.Contains(menuNumber))
                    currentItems.Remove(menuNumber);
                else
                    return false;
            }

            return true;
        }

    }
}