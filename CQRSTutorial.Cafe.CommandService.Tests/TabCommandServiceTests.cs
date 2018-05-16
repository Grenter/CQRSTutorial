using CQRSTutorial.Cafe.Messaging;
using MassTransit;
using NSubstitute;
using NUnit.Framework;

namespace CQRSTutorial.Cafe.CommandService.Tests
{
    [TestFixture]
    public class TabCommandServiceTests
    {
        private TabCommandService _tabCommandService;
        private IBusControl _busControl;
        private IMessageBus _messageBus;

        [SetUp]
        public void SetUp()
        {
            _busControl = Substitute.For<IBusControl>();
            _messageBus = Substitute.For<IMessageBus>();
            _messageBus.Create().Returns(_busControl);
            _tabCommandService = new TabCommandService(_messageBus);
        }

        [Test]
        public void Start_tab_service_starts_message_bus()
        {
            _tabCommandService.Start();

            _busControl.Received(1).Start();
        }

        [Test]
        public void Stop_tab_service_stops_message_bus()
        {
            _tabCommandService.Start();
            _tabCommandService.Stop();

            _busControl.Received(1).Stop();
        }
    }
}
