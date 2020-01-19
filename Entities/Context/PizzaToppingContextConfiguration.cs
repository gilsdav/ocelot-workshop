using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaGraphQL.Entities.Context
{
    public class PizzaToppingContextConfiguration : IEntityTypeConfiguration<PizzaTopping>
    {
        public void Configure(EntityTypeBuilder<PizzaTopping> builder)
        {
            builder
                .HasData(
                new PizzaTopping
                {
                    PizzaId = 1,
                    ToppingId = 1
                },
                new PizzaTopping
                {
                    PizzaId = 1,
                    ToppingId = 2
                },
                new PizzaTopping
                {
                    PizzaId = 2,
                    ToppingId = 2
                },
                new PizzaTopping
                {
                    PizzaId = 3,
                    ToppingId = 3
                }
           );
        }
    }
}