using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Repositories;

namespace PizzaGraphQL.GraphQL
{
    public class PizzaQuery: ObjectGraphType
    {
        public PizzaQuery(IPizzaRepository pizzaRepository, IToppingRepository toppingRepository) {
            Field<PizzaType>(
                "pizza",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context => this.loadPizza(context, pizzaRepository));
            
            Field<ListGraphType<PizzaType>>(
                "pizzas",
                resolve: context => this.loadAllPizzas(context, pizzaRepository));

            Field<ListGraphType<ToppingType>>(
                "toppings",
                resolve: context => toppingRepository.GetAll());
        }

        private Pizza loadPizza(IResolveFieldContext<object> context, IPizzaRepository pizzaRepository) {
            int id = (int)context.Arguments["id"];
            var loadToppings = context.SubFields.FirstOrDefault(kv => kv.Key == "toppings").Key != null;
            return pizzaRepository.GetById(id, loadToppings);
        }

        private IEnumerable<Pizza> loadAllPizzas(IResolveFieldContext<object> context, IPizzaRepository pizzaRepository) {
            var loadToppings = context.SubFields.FirstOrDefault(kv => kv.Key == "toppings").Key != null;
            return pizzaRepository.GetAll(loadToppings);
        }
    }
}