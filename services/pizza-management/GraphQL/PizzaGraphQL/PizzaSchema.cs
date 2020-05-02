using GraphQL.Types;

namespace PizzaGraphQL.GraphQL.PizzaGraphQL
{
    public class PizzaSchema: Schema
    {
        public PizzaSchema(PizzaQuery query, PizzaMutation mutation, PizzaSubscription subscription)
        {
            Query = query;
            Mutation = mutation;
            Subscription = subscription;
        }
    }
}