using System;
using Microsoft.EntityFrameworkCore;

namespace PizzaGraphQL.Entities.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PizzaContextConfiguration());
            modelBuilder.ApplyConfiguration(new ToppingContextConfiguration());
            modelBuilder.Entity<PizzaTopping>().HasKey(pt => new { pt.PizzaId, pt.ToppingId });
            modelBuilder.ApplyConfiguration(new PizzaToppingContextConfiguration());
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<PizzaTopping> PizzaToppings { get; set; }
    }
}