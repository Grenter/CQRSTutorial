using CQRSTutorial.Cafe.Common;
using MassTransit;
using System;
using System.Threading.Tasks;
using CQRSTutorial.Cafe.Domain;

namespace CQRSTutorial.Cafe.CommandService.Consumers
{
    public abstract class Consumer<TCommand> : IConsumer<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public Consumer(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        
        public async Task Consume(ConsumeContext<TCommand> context)
        {
            _commandDispatcher.DispatchCommand(context.Message);
        }
    }
}
