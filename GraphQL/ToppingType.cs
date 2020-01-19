using GraphQL.Types;
using PizzaGraphQL.Entities;

namespace PizzaGraphQL.GraphQL
{
    public class ToppingType : ObjectGraphType<Topping>
    {
        public ToppingType() {
            Field(x => x.Id);
            Field(x => x.Name, true).Description("Name of topping");
        }
    }
}