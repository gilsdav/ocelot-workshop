using GraphQL.Types;

namespace PizzaGraphQL.GraphQL.PizzaGraphQL
{
    public class PizzaInputType : InputObjectGraphType
    {
        public PizzaInputType()
        {
            Name = "PizzaInputType";
            Field<IntGraphType>("id");
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<ListGraphType<IntGraphType>>>("toppings");
        }
    }
}