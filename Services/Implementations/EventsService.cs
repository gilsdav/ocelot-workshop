using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using PizzaGraphQL.Entities;

namespace PizzaGraphQL.Services.Implementations
{
    public class EventsService : IEventsService
    {
        private readonly ISubject<Pizza> _pizzaStream = new ReplaySubject<Pizza>(1);

        public IObservable<Pizza> ListenPizzaChanges()
        {
            return _pizzaStream
                .Select(pizza =>
                {
                    return pizza;
                })
                .AsObservable();
        }

        public void EmitPizzaChange(Pizza pizza) {
            _pizzaStream.OnNext(pizza);
        }

    }
}