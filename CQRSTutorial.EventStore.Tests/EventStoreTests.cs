using CQRSTutorial.Events;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

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

            var eventsAssembly = Assembly.LoadFile(@"C:\code\CQRSTutorial\CQRSTutorial.Events\bin\Debug\netstandard2.0\CQRSTutorial.Events.dll");

            _storeRepository = new EventRepository(DbContext, eventsAssembly);
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
        public void Get_events_for_aggregate_id_returns_events()
        {
            _storeRepository.Add(GenerateTabOpened());

            var events = _storeRepository.GetEventsFor(_aggregateId);

            Assert.That(events.Count(), Is.EqualTo(1));
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
