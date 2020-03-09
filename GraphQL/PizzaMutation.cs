using System.Collections.Generic;
using GraphQL.Types;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Repositories;
using System.Linq;

namespace PizzaGraphQL.GraphQL
{
    public class PizzaMutation: ObjectGraphType
    {
        public PizzaMutation(IPizzaRepository pizzaRepository) {
            Name = "MyMutationName";

            Field<PizzaType>(
                "createPizza",
                arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<PizzaInputType>> { Name = "pizza" }
            ),
            resolve: context =>
            {
                dynamic pizzaInput = context.Arguments["pizza"];
                string name = pizzaInput["name"];
                List<object> toppingKeys = pizzaInput["toppings"];
                var pizza = new Pizza() {
                    Name = name,
                    PizzaToppings = toppingKeys.Select(tk => new PizzaTopping(){ ToppingId = (int) tk }).ToList()
                };
                pizzaRepository.Add(pizza);
                return this.loadPizza(pizza.Id, context, pizzaRepository);
            });
        }

        private Pizza loadPizza(int id, IResolveFieldContext<object> context, IPizzaRepository pizzaRepository) {
            var loadToppings = context.SubFields.FirstOrDefault(kv => kv.Key == "toppings").Key != null;
            return pizzaRepository.GetById(id, loadToppings);
        }
    }
}