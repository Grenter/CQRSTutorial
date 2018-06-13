using System.Collections.Generic;
using CQRSTutorial.Events;

namespace CQRSTutorial.Core
{
    public abstract class Aggregate
    {
        protected IList<IDomainEvent> Events { get; private set; }

        protected Aggregate()
        {
            Events = new List<IDomainEvent>();
        }
    }
}
