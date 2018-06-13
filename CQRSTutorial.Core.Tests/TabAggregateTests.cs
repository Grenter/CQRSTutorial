using CQRSTutorial.Events;
using NUnit.Framework;
using System;
using System.Linq;

namespace CQRSTutorial.Core.Tests
{
    [TestFixture]
    public class TabAggregateTests
    {
        private Guid _tabId;

        [SetUp]
        public void SetUp()
        {
            _tabId = new Guid("F747B574-5BB2-46E1-BEF1-A86AEFEA8ECE");
        }

        [Test]
        public void When_new_tab_aggregate_tab_opened_event_raised()
        {
            var tab = new TabAggregate(_tabId, 45, "Gary");

            var tabEvent = tab.GetDomainEvents().Last() as TabOpened;

            Assert.That(tabEvent, Is.TypeOf<TabOpened>());
            Assert.That(tabEvent.AggregateId, Is.EqualTo(_tabId));
            Assert.That(tabEvent.TableNumber, Is.EqualTo(45));
            Assert.That(tabEvent.WaiterName, Is.EqualTo("Gary"));
            Assert.That(tabEvent.Balance, Is.EqualTo(0.0m));
        }
    }
}
