﻿using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Common;
using CQRSTutorial.Cafe.Events;
using System.Collections;
using System.Linq;

namespace CQRSTutorial.Cafe.Domain
{
    public class TabAggregate : Aggregate,
        ICommandHander<OpenTabCommand>,
        ICommandHander<PlaceOrderCommand>,
        ICommandHander<ServeDrinksCommand>,
        IApplyEvent<TabOpened>,
        IApplyEvent<DrinksOrdered>
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

        public IEnumerable Handle(ServeDrinksCommand c)
        {
            yield return new DrinksServed
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
            
        }
    }
}