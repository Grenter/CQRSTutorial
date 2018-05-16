using System;
using System.Threading.Tasks;
using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Web.Controllers;
using CQRSTutorial.Cafe.Web.Messaging;
using MassTransit;
using NSubstitute;
using NUnit.Framework;

namespace CQRSTutorial.Cafe.Web.Tests
{
    [TestFixture]
    public class TabControllerTests
    {
        private TabController _tabController;
        private CommandSender _commandSender;
        private string _waiter;
        private int _tableNumber;
        private ISendEndpoint _sendEndPoint;
        private IEndPointProvider _endPointProvider;

        [SetUp]
        public void Setup()
        {
            _waiter = "David Grenter";
            _tableNumber = 12;
            _sendEndPoint = Substitute.For<ISendEndpoint>();
            _endPointProvider = Substitute.For<IEndPointProvider>();
            _endPointProvider.GetEndpoint(Arg.Is<string>(queueName => queueName == "cafe.waiter.command.service")).Returns(Task.FromResult(_sendEndPoint));
            _commandSender = new CommandSender(_endPointProvider);
        }

        [Test]
        public async Task Open_tab_command_sentAsync()
        {
            await CreateTab();
            await _sendEndPoint.Received().Send(Arg.Is<OpenTabCommand>(c => c.Id != Guid.Empty && c.Waiter == _waiter && c.TableNumber == _tableNumber));
        }

        private async Task CreateTab()
        {
            _tabController = new TabController(_commandSender);
            await _tabController.Create(new OpenTabDto
            {
                Waiter = _waiter,
                TableNumer = _tableNumber
            });
        }
    }
}
