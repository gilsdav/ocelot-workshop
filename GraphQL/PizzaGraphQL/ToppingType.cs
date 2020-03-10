using GraphQL.Types;
using PizzaGraphQL.Entities;

namespace PizzaGraphQL.GraphQL.PizzaGraphQL
{
    public class ToppingType : ObjectGraphType<Topping>
    {
        public ToppingType() {
            Field(x => x.Id);
            Field(x => x.Name, false).Description("Name of topping");
        }
    }
}