using System;
using System.Collections.Generic;

namespace pizza_commands
{
    public class Command
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public IEnumerable<int> Pizzas { get; set; }
    }
}
