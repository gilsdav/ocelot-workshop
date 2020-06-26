using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pizza_commands.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ILogger<CommandsController> _logger;

        public CommandsController(ILogger<CommandsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Command> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Command
            {
                Date = DateTime.Now,
                Id = index,
                Pizzas = new[] { rng.Next(1, 10), rng.Next(1, 10) }
            })
            .ToArray();
        }

        [HttpPost]
        public Command Post(Command command)
        {
            command.Id = 10;
            return command;
        }

        [HttpGet("customer")]
        public string GetCurrentCustomer()
        {
            return Request.Headers["CustomerId"];
        }


        [HttpGet("headers")]
        public IHeaderDictionary GetHeaders()
        {
            return Request.Headers;
        }

    }
}
