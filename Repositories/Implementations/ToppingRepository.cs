using System.Collections.Generic;
using System.Linq;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Entities.Context;

namespace PizzaGraphQL.Repositories.Implmentations
{
    public class ToppingRepository : IToppingRepository
    {
        private readonly ApplicationContext _context;

        public ToppingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Topping> GetAll() => _context.Toppings.ToList();
        
    }
}