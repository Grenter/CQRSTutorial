using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Web.Controllers;
using MassTransit;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using CQRSTutorial.Cafe.Messaging;
using ISendEndpointProvider = CQRSTutorial.Cafe.Messaging.ISendEndpointProvider;

namespace CQRSTutorial.Cafe.Web.Tests
{
    [TestFixture]
    public class TabControllerTests
    {
        private TabController _tabController;
        private CommandSender _commandSender;
        private ISendEndpoint _sendEndPoint;
        private ISendEndpointProvider _sendEndpointProvider;
        private OpenTabDto _openTabDto;
        private ISendEndpointConfiguration _endpointConfiguration;

        [SetUp]
        public void Setup()
        {
            _sendEndPoint = Substitute.For<ISendEndpoint>();
            _sendEndpointProvider = Substitute.For<ISendEndpointProvider>();

            _endpointConfiguration = Substitute.For<ISendEndpointConfiguration>();
            _endpointConfiguration.Queue.Returns("cafe.waiter.command.service");

            _sendEndpointProvider.GetEndpoint(Arg.Is<string>(queueName => queueName == _endpointConfiguration.Queue))
                                    .Returns(Task.FromResult(_sendEndPoint));
            _commandSender = new CommandSender(_sendEndpointProvider, _endpointConfiguration);
            _openTabDto = CreateOpenTabDto();
        }

        [Test]
        public async Task Open_tab_command_sentAsync()
        {
            await CreateTab();
            await _sendEndPoint.Received().Send(Arg.Is<OpenTabCommand>(c => CheckCommand(c)));
        }

        private bool CheckCommand(OpenTabCommand c)
        {
            return c.Id != Guid.Empty
                   && c.Waiter == _openTabDto.Waiter
                   && c.TableNumber == _openTabDto.TableNumber;
        }

        private async Task CreateTab()
        {
            _tabController = new TabController(_commandSender);
            await _tabController.Create(_openTabDto);
        }

        private OpenTabDto CreateOpenTabDto()
        {
            return new OpenTabDto
            {
                Waiter = "David Grenter",
                TableNumber = 12
            };
        }
    }
}
