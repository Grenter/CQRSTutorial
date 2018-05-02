using System;
using System.Collections.Generic;
using CQRSTutorial.Cafe.Common;
using CQRSTutorial.Cafe.Domain.Commands;
using CQRSTutorial.Cafe.Events;
using NUnit.Framework;

namespace CQRSTutorial.Cafe.Domain.Tests
{
    public class TabTests : BaseAggregateTests<TabAggregate>
    {
        private Guid _testId;
        private int _testTable;
        private string _testWaiter;
        private OrderedItem _testDrink1;
        private OrderedItem _testDrink2;
        private OrderedItem _testFood1;
        private OrderedItem _testFood2;

        [SetUp]
        public void Setup()
        {
            _testId = Guid.NewGuid();
            _testTable = 1;
            _testWaiter = "Rupert";
            _testDrink1 = new OrderedItem
            {
                Description = "Thatchers Cider (pint)",
                IsDrink = true,
                MenuNumber = 1,
                Price = 4.5m
            };
            _testDrink2 = new OrderedItem
            {
                Description = "Coke (half)",
                IsDrink = true,
                MenuNumber = 1,
                Price = 1.5m
            };
            _testFood1 = new OrderedItem
            {
                Description = "Bacon Cheeseburger",
                Price = 10m,
                MenuNumber = 1,
                IsDrink = false
            };
            _testFood2 = new OrderedItem
            {
                Description = "Falafel Salad",
                Price = 7.5m,
                MenuNumber = 1,
                IsDrink = false
            };
        }

        [Test]
        public void Can_open_a_new_tab()
        {
            Test(
                Given(),
                When(new OpenTabCommand
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }),
                Then(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }
                ));
        }

        [Test]
        public void Can_not_order_with_unopened_tab()
        {
            Test(
                Given(),
                When(new PlaceOrderCommand
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testDrink1 }
                }),
                ThenFailWith<TabNotOpen>());
        }

        [Test]
        public void Can_place_drinks_order()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }),
                When(new PlaceOrderCommand
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testDrink1, _testDrink2 }
                }),
                Then(new DrinksOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testDrink1, _testDrink2 }
                }));
        }

        [Test]
        public void Can_place_food_order()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }),
                When(new PlaceOrderCommand
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testFood1, _testFood2 }
                }),
                Then(new FoodOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testFood1, _testFood2 }
                }));
        }

        [Test]
        public void Can_place_food_and_drink_order()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }),
                When(new PlaceOrderCommand
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testFood1, _testDrink2 }
                }),
                Then(new DrinksOrdered
                    {
                        Id = _testId,
                        Items = new List<OrderedItem> { _testDrink2 }
                    },
                    new FoodOrdered
                    {
                        Id = _testId,
                        Items = new List<OrderedItem> { _testFood1 }
                    }));
        }
    }
}
