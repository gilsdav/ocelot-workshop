using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.EntityFrameworkCore;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Entities.Context;
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

        

        private IQueryable<Pizza> AddToppingsIntoPizzaQuery(IQueryable<Pizza> query)
        {
            return query.Include(p => p.PizzaToppings)
                        .ThenInclude(pt => pt.Topping);
        }

    }
}