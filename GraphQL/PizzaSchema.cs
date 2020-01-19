using System;
using GraphQL.Types;

namespace PizzaGraphQL.GraphQL
{
    public class PizzaSchema: Schema
    {
        public PizzaSchema(PizzaQuery query, PizzaMutation mutation)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}