using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.EntityFrameworkCore;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Entities.Context;

namespace PizzaGraphQL.Repositories.Implmentations
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ApplicationContext _context;
        private readonly ISubject<Pizza> _pizzaStream = new ReplaySubject<Pizza>(1);

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

        public Pizza Add(Pizza newPizza)
        {
            var command = _context.Pizzas.Add(newPizza);
            _context.SaveChanges();
            _pizzaStream.OnNext(command.Entity);
            return command.Entity;
        }

        public IObservable<Pizza> ListenPizzaChanges()
        {
            return _pizzaStream
                .Select(pizza =>
                {
                    return pizza;
                })
                .AsObservable();
        }

        private IQueryable<Pizza> AddToppingsIntoPizzaQuery(IQueryable<Pizza> query)
        {
            return query.Include(p => p.PizzaToppings)
                        .ThenInclude(pt => pt.Topping);
        }

    }
}