using CQRSTutorial.Core;
using System.Collections.Generic;

namespace CQRSTutorial.Domain
{
    public abstract class Aggregate : IAggregate
    {
        protected IList<IDomainEvent> Events { get; private set; }

        protected Aggregate()
        {
            Events = new List<IDomainEvent>();
        }

        public abstract IEnumerable<IDomainEvent> GetDomainEvents();
        public abstract void Apply(IDomainEvent domainEvent);
    }

    public interface IAggregate
    {
        void Apply(IDomainEvent domainEvent);
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}
