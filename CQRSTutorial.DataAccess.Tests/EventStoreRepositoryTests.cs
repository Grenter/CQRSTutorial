using CQRSTutorial.DataAccess.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace CQRSTutorial.DataAccess.Tests
{
    [TestFixture]
    public class EventStoreRepositoryTests
    {
        protected EventStoreContext DbContext;
        private SqliteConnection _connection;
        private Guid _aggregateId = new Guid("c43eebf5-22b6-4ede-b085-39333b9700c4");

        [SetUp]
        public void SetUp()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<EventStoreContext>()
                .UseSqlite(_connection)
                .Options;

            DbContext = new EventStoreContext(options);

            DbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }

        [Test]
        public void Ensure_event_created()
        {
            DbContext.Events.Add(new Event());
            DbContext.SaveChanges();

            var @event = DbContext.Events.FirstOrDefault();

            Assert.That(@event?.Id, Is.TypeOf(typeof(Guid)));
        }
    }
}
