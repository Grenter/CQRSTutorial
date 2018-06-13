using CQRSTutorial.Core.Events;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace CQRSTutorial.EventStore.Tests
{
    [TestFixture]
    public class EventStoreTests
    {
        private Guid _aggregateId;
        protected EventStoreContext DbContext;
        private SqliteConnection _connection;
        private EventRepository _storeRepository;

        [SetUp]
        public void SetUp()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _aggregateId = new Guid("FC5B2701-E9B4-41E5-BD73-0C37185ADCBB");
            
           _connection.Open();

            var options = new DbContextOptionsBuilder<EventStoreContext>()
                .UseSqlite(_connection)
                .Options;

            DbContext = new EventStoreContext(options);

            _storeRepository = new EventRepository(DbContext);
        }

        [Test]
        public void Save_event_to_store()
        {
            var tabOpened = GenerateTabOpened();
            _storeRepository.Add(tabOpened);

            var events = DbContext.Events.Count(ev => ev.AggregateId == _aggregateId);

            Assert.That(events, Is.EqualTo(1));
        }

        [Test]
        public void Get_all_events_for_aggregate_id()
        {
            _storeRepository.Add(GenerateTabOpened());

            var domainEvents = _storeRepository.GetAllEvents(_aggregateId);

            Assert.That(domainEvents.Count(), Is.EqualTo(1));
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }

        private TabOpened GenerateTabOpened()
        {
            var tabOpened = new TabOpened
            {
                Id = Guid.NewGuid(),
                AggregateId = _aggregateId,
                TableNumber = 45,
                WaiterName = "Sue"
            };
            return tabOpened;
        }
    }
}
