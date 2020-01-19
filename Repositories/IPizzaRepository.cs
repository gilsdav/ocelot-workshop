using System.Collections.Generic;
using GraphQL.Types;
using PizzaGraphQL.Entities;

namespace PizzaGraphQL.Repositories
{
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> GetAll(bool loadToppings);
        Pizza GetById(int id, bool loadToppings);
        Pizza Add(Pizza newPizza);
    }
}