using System;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using PizzaGraphQL.Entities;
using PizzaGraphQL.Services;

namespace PizzaGraphQL.GraphQL.PizzaGraphQL
{
    public class PizzaSubscription : ObjectGraphType
    {
        private readonly IEventsService _eventsService;

        public PizzaSubscription(IEventsService eventsService)
        {
            _eventsService = eventsService;
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
            return _eventsService.ListenPizzaChanges();
        }
    }
}