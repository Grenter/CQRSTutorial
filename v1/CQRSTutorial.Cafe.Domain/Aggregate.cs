using System;
using System.Collections;
using CQRSTutorial.Cafe.Common;

namespace CQRSTutorial.Cafe.Domain
{
    public class Aggregate
    {
        public int EventsLoaded { get; private set; }

        public Guid Id { get; internal set; }

        public void ApplyEvents(IEnumerable events)
        {
            foreach (var e in events)
                GetType().GetMethod("ApplyOneEvent")
                    .MakeGenericMethod(e.GetType())
                    .Invoke(this, new object[] { e });
        }

        public void ApplyOneEvent<TEvent>(TEvent ev)
        {
            var applier = this as IApplyEvent<TEvent>;
            if (applier == null)
                throw new InvalidOperationException(string.Format(
                    "Aggregate {0} does not know how to apply event {1}",
                    GetType().Name, ev.GetType().Name));
            applier.Apply(ev);
            EventsLoaded++;
        }
    }
}
