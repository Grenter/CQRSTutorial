using CQRSTutorial.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace CQRSTutorial.DataAccess
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options) 
            : base (options)
        { }

        public DbSet<Event> Events { get; set; }
    }
}
