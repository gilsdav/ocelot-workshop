using System;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Repositories;

namespace PizzaGraphQL.GraphQL
{
    public class PizzaSubscription : ObjectGraphType
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaSubscription(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
            AddField(new EventStreamFieldType
            {
                Name = "pizzaAdded",
                Type = typeof(PizzaType),
                Resolver = new FuncFieldResolver<Pizza>(ResolvePizza),
                Subscriber = new EventStreamResolver<Pizza>(Subscribe)
            });
        }

        private Pizza ResolvePizza(IResolveFieldContext context)
        {
            return context.Source as Pizza;
        }

        private IObservable<Pizza> Subscribe(IResolveEventStreamContext context)
        {
            return _pizzaRepository.ListenPizzaChanges();
        }
    }
}