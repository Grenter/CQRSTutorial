using System;
using System.Collections;

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
    }
}
