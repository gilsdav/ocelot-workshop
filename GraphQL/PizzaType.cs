using GraphQL.Types;
using PizzaGraphQL.Entities;
using System.Linq;

namespace PizzaGraphQL.GraphQL
{
    public class PizzaType : ObjectGraphType<Pizza>
    {
        public PizzaType() {
            Field(x => x.Id);
            Field(x => x.Name, false).Description("Name of pizza");
            Field<ListGraphType<ToppingType>>("toppings", resolve: context => context.Source.PizzaToppings.Select(pt => pt.Topping).ToList());
        }
    }
}