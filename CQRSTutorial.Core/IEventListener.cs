using System.Collections.Generic;

namespace CQRSTutorial.Core
{
    public interface IEventListener
    {
        void Handle(IDomainEvent domainEvents);
    }
}
