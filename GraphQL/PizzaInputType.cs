using GraphQL.Types;

namespace PizzaGraphQL.GraphQL
{
    public class PizzaInputType : InputObjectGraphType
    {
        public PizzaInputType()
        {
            Name = "PizzaInputType";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<ListGraphType<IntGraphType>>("toppings");
        }
    }
}