using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Entities.Context;

namespace PizzaGraphQL.Repositories.Implmentations
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ApplicationContext _context;

        public PizzaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Pizza> GetAll(bool loadToppings)
        {
            IQueryable<Pizza> query = _context.Pizzas;
            if (loadToppings) {
                query = AddToppingsIntoPizzaQuery(query);
            }
            return query.AsNoTracking().ToList();
        }
        public Pizza GetById(int id, bool loadToppings)
        {
            IQueryable<Pizza> query = _context.Pizzas;
            if (loadToppings) {
                query = AddToppingsIntoPizzaQuery(query);
            }
            return query.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        private IQueryable<Pizza> AddToppingsIntoPizzaQuery(IQueryable<Pizza> query)
        {
            return query.Include(p => p.PizzaToppings)
                        .ThenInclude(pt => pt.Topping);
        }

    }
}