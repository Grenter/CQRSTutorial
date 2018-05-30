using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Domain;

namespace CQRSTutorial.Cafe.CommandService.Consumers
{
    public class OpenTabConsumer : Consumer<OpenTabCommand>
    {
        public OpenTabConsumer(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }
    }
}