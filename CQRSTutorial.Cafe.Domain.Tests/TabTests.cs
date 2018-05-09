using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Common;
using CQRSTutorial.Cafe.Events;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
                MenuNumber = 50,
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
                MenuNumber = 100,
                IsDrink = false
            };
            _testFood2 = new OrderedItem
            {
                Description = "Falafel Salad",
                Price = 7.5m,
                MenuNumber = 130,
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

        [Test]
        public void Ordered_drinks_can_be_served()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }, new DrinksOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testDrink1, _testDrink2 }
                }),
                When(new ServeDrinksCommand
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testDrink1.MenuNumber, _testDrink2.MenuNumber }
                }),
                Then(new DrinksServed
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testDrink1.MenuNumber, _testDrink2.MenuNumber }
                }));
        }

        [Test]
        public void Can_not_serve_an_unordered_drink()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }, new DrinksOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testDrink1 }
                }),
                When(
                    new ServeDrinksCommand
                    {
                        Id = _testId,
                        MenuNumbers = new List<int> { _testDrink2.MenuNumber }
                    }),
                ThenFailWith<DrinksNotOutstanding>());
        }

        [Test]
        public void Can_not_serve_an_ordered_drink_twice()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }, new DrinksOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testDrink1 }
                }, new DrinksServed
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testDrink1.MenuNumber }
                }),
                When(new ServeDrinksCommand
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testDrink1.MenuNumber }
                }),
                ThenFailWith<DrinksNotOutstanding>());
        }

        [Test]
        public void Ordered_food_can_be_served()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }, new FoodOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testFood1, _testFood2 }
                }),
                When(new ServeFoodCommand
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testFood1.MenuNumber, _testFood2.MenuNumber }
                }),
                Then(new FoodServed
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testFood1.MenuNumber, _testFood2.MenuNumber }
                }));
        }

        [Test]
        public void Can_not_serve_an_unordered_food_item()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }, new FoodOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testFood1 }
                }),
                When(
                    new ServeFoodCommand
                    {
                        Id = _testId,
                        MenuNumbers = new List<int> { _testFood2.MenuNumber }
                    }),
                ThenFailWith<FoodNotOutstanding>()); 
        }

        [Test]
        public void Can_not_serve_an_unordered_food_item_twice()
        {
            Test(
                Given(new TabOpened
                {
                    Id = _testId,
                    TableNumber = _testTable,
                    Waiter = _testWaiter
                }, new FoodOrdered
                {
                    Id = _testId,
                    Items = new List<OrderedItem> { _testFood1 }
                }, new FoodServed
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testFood1.MenuNumber }
                }),
                When(new ServeFoodCommand
                {
                    Id = _testId,
                    MenuNumbers = new List<int> { _testFood1.MenuNumber }
                }),
                ThenFailWith<FoodNotOutstanding>());
        }

        [Test]
        public void Can_close_tab_with_tip()
        {
            Test(
                Given(new TabOpened
                    {
                        Id = _testId,
                        TableNumber = _testTable,
                        Waiter = _testWaiter
                    },
                    new DrinksOrdered
                    {
                        Id = _testId,
                        Items = new List<OrderedItem> { _testDrink2 }
                    },
                    new DrinksServed
                    {
                        Id = _testId,
                        MenuNumbers = new List<int> { _testDrink2.MenuNumber }
                    }),
                When(new CloseTabCommand
                    {
                        Id = _testId,
                        AmmountPaid = _testDrink2.Price + 0.50M
                    }),
                Then (new TabClosed
                {
                    Id = _testId,
                    AmmountPaid = _testDrink2.Price + 0.50M,
                    OrderValue = _testDrink2.Price,
                    TipValue = 0.50M
                }));
        }
    }
}
