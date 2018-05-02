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
        private Guid testId;
        private int testTable;
        private string testWaiter;
        private OrderedItem testDrink1;
        private OrderedItem testDrink2;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testTable = 1;
            testWaiter = "Rupert";
            testDrink1 = new OrderedItem
            {
                Description = "Thatchers Cider (pint)",
                IsDrink = true,
                MenuNumber = 1,
                Price = 4.5m
            };
            testDrink2 = new OrderedItem
            {
                Description = "Coke (half)",
                IsDrink = true,
                MenuNumber = 1,
                Price = 1.5m
            };
        }

        [Test]
        public void Can_open_a_new_tab()
        {
            Test(
                Given(),
                When(new OpenTabCommand
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                Then(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
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
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1 }
                }),
                ThenFailWith<TabNotOpen>());
        }

        [Test]
        public void Can_place_drinks_order()
        {
            Test(
                Given(new TabOpened
                {
                    Id = testId,
                    TableNumber = testTable,
                    Waiter = testWaiter
                }),
                When(new PlaceOrderCommand
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1, testDrink2 }
                }),
                Then(new DrinksOrdered
                {
                    Id = testId,
                    Items = new List<OrderedItem> { testDrink1, testDrink2 }
                }));
        }
    }
}
