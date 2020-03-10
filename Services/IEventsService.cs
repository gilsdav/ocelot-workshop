using System;
using PizzaGraphQL.Entities;

namespace PizzaGraphQL.Services
{
    public interface IEventsService
    {
        IObservable<Pizza> ListenPizzaChanges();
        void EmitPizzaChange(Pizza pizza);
    }
}