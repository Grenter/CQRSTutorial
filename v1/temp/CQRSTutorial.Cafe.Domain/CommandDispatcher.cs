using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CQRSTutorial.Cafe.Common;

namespace CQRSTutorial.Cafe.Domain
{
    public interface ICommandDispatcher
    {
        IEnumerable DispatchCommand<TCommand>(TCommand c);
    }

    public class CommandDispatcher<TAggregate> : ICommandDispatcher
        where TAggregate : Aggregate, new ()
    {
        private readonly TAggregate _aggregate;

        public CommandDispatcher(TAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public IEnumerable DispatchCommand<TCommand>(TCommand c)
        {
            var handler = _aggregate as ICommandHandler<TCommand>;

            return handler?.Handle(c);
        }
    }
}