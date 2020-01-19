using GraphQL.Types;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Repositories;

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
            )/*,
            resolve: context =>
            {
                var pizza = context.GetArgument<Pizza>("pizza");
                // return playerRepository.Add(player);
                return pizza;
            }*/);
        }
    }
}