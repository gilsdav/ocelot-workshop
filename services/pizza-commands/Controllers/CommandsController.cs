using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pizza_commands.BusinessLogic.Interfaces;

namespace pizza_commands.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ILogger<CommandsController> _logger;
        private readonly ICommandsBL _commandsBL;

        public CommandsController(ILogger<CommandsController> logger, ICommandsBL commandsBL)
        {
            _logger = logger;
            _commandsBL = commandsBL;
        }

        [HttpGet]
        public IEnumerable<Command> Get()
        {
            Console.WriteLine("JE VEUX DES PIZZAS !!");
           return this._commandsBL.GetCommands();
        }

        [HttpGet("{id}")]
        public Command GetById(int id)
        {
            return this._commandsBL.GetCommand(id);
        }

        [HttpPost]
        public Command Post(Command command)
        {
            return this._commandsBL.AddCommand(command);
        }

        [HttpGet("customer")]
        public string GetCurrentCustomer()
        {
            return Request.Headers["CustomerId"];
        }


        /*
            This get is just to show the headers inside the request
        */
        [HttpGet("headers")]
        public IHeaderDictionary GetHeaders()
        {
            return Request.Headers;
        }

    }
}
