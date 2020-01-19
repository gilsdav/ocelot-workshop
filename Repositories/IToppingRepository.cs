using System.Collections.Generic;
using PizzaGraphQL.Entities;

namespace PizzaGraphQL.Repositories
{
    public interface IToppingRepository
    {
        IEnumerable<Topping> GetAll();
    }
}