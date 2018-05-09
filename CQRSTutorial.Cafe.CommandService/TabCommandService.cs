﻿using MassTransit;

namespace CQRSTutorial.Cafe.CommandService
{
    public class TabCommandService
    {
        private IBusControl _busControl;
        private IMessageBus _messageBus;

        public TabCommandService(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Start()
        {
            _busControl = _messageBus.Create();
            _busControl.Start();
        }
    }
}
