using CQRSTutorial.Cafe.Commands;
using CQRSTutorial.Cafe.Web.Messaging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CQRSTutorial.Cafe.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Tab")]
    public class TabController : Controller
    {
        private readonly ICommandSender _commandSender;

        public TabController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create([FromBody]OpenTabDto openTabDto)
        {
            var openTabCommand = new OpenTabCommand
            {
                Id = Guid.NewGuid(),
                Waiter = openTabDto.Waiter,
                TableNumber = openTabDto.TableNumer
            };

            await _commandSender.Send(openTabCommand);
        }
    }

    public class OpenTabDto
    {
        public string Waiter { get; set; }
        public int TableNumer { get; set; }
    }
}