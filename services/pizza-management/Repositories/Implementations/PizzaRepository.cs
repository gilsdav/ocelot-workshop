using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Entities.Context;
using PizzaGraphQL.Repositories.Implementations;
using PizzaGraphQL.Services;

namespace PizzaGraphQL.Repositories.Implmentations
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ApplicationContext _context;
        private readonly IEventsService _eventsService;

        public PizzaRepository(ApplicationContext context, IEventsService eventsService)
        {
            _context = context;
            _eventsService = eventsService;
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

        public Pizza Add(Pizza newPizza)
        {
            var command = _context.Pizzas.Add(newPizza);
            _context.SaveChanges();
            _eventsService.EmitPizzaChange(command.Entity);
            return command.Entity;
        }

        public Pizza Update(Pizza pizza) {
            // Remove all toppings links
            var pizzaFromDB = this.GetById(pizza.Id, true);
            _context.TryUpdateManyToMany(
                pizzaFromDB.PizzaToppings,
                pizza.PizzaToppings,
                pt => $"{pt.PizzaId}_{pt.ToppingId}"
            );
            // Update Pizza
            pizzaFromDB.Name = pizza.Name;
            var command = _context.Pizzas.Update(pizzaFromDB);
            _context.SaveChanges();
            _eventsService.EmitPizzaChange(command.Entity);
            return command.Entity;
        }

        public void Delete(int pizzaId) {
            var pizzaFromDB = this.GetById(pizzaId, true);
            var command = _context.Pizzas.Remove(pizzaFromDB);
            _context.SaveChanges();
        }

        

        private IQueryable<Pizza> AddToppingsIntoPizzaQuery(IQueryable<Pizza> query)
        {
            return query.Include(p => p.PizzaToppings)
                        .ThenInclude(pt => pt.Topping);
        }

    }
}