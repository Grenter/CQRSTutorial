using CQRSTutorial.Cafe.Common;

namespace CQRSTutorial.Cafe.Domain
{
    public interface ICommandDispatcher
    {
        void DispatchCommand<TCommand>(TCommand c);
    }

    public class CommandDispatcher<TAggregate> : ICommandDispatcher
        where TAggregate : Aggregate, new ()
    {
        private readonly TAggregate _aggregate;

        public CommandDispatcher(TAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public void DispatchCommand<TCommand>(TCommand c)
        {
            var handler = _aggregate as ICommandHandler<TCommand>;
            var events = handler?.Handle(c);

            // will call eventhandler from here.
        }
    }
}