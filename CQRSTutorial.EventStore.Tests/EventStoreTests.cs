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
        
        [Test]
        public void Save_event_to_store()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _aggregateId = new Guid("FC5B2701-E9B4-41E5-BD73-0C37185ADCBB");
            
            var tabOpened = new TabOpened
            {
                Id = Guid.NewGuid(),
                AggregateId = _aggregateId,
                TableNumber = 45,
                WaiterName = "Sue"
            };
           
           _connection.Open();
            var options = new DbContextOptionsBuilder<EventStoreContext>()
                .UseSqlite(_connection)
                .Options;

            DbContext = new EventStoreContext(options);

            var storeRepository = new EventRepository(DbContext);
            storeRepository.Add<TabOpened>(tabOpened);

            var events = DbContext.Events.Count(ev => ev.AggregateId == _aggregateId);

            Assert.That(events, Is.EqualTo(1));

            _connection.Close();
        }
    }
}
