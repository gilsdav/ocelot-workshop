using System;
using GraphQL.Types;

namespace PizzaGraphQL.GraphQL
{
    public class BobSchema: Schema
    {
        public BobSchema(PizzaQuery query, PizzaMutation mutation)
        {
            Query = query;
        }
    }
}