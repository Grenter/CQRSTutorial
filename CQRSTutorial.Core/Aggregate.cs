using System.Collections.Generic;
using CQRSTutorial.Core.Events;

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
