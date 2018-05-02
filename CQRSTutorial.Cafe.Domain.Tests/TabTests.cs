using System;
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

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testTable = 1;
            testWaiter = "Rupert";
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
    }
}
