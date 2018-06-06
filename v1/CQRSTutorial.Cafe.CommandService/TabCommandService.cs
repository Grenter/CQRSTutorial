using CQRSTutorial.Cafe.Messaging;
using MassTransit;

namespace CQRSTutorial.Cafe.CommandService
{
    public class TabCommandService
    {
        private IBusControl _busControl;
        private readonly IMessageBus _messageBus;

        public TabCommandService(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Start()
        {
            _busControl = _messageBus.Create();
            _busControl.Start();
        }

        public void Stop()
        {
            _busControl.Stop();
        }
    }
}