using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pizza_commands.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly ILogger<PricesController> _logger;

        public PricesController(ILogger<PricesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Price> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Price
            {
                Id = index,
                Pizza = index,
                Amount = rng.Next(1, 30)
            })
            .ToArray();
        }
        
        [HttpPost]
        public Price Post(Price price)
        {
            price.Id = 10;
            return price;
        }

    }
}
