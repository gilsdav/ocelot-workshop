using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaGraphQL.Entities.Context
{
    public class PizzaContextConfiguration : IEntityTypeConfiguration<Pizza>
    {
        public void Configure(EntityTypeBuilder<Pizza> builder)
        {
            builder
                .HasData(
                new Pizza
                {
                    Id = 1,
                    Name = "4 fromage"
                },
                new Pizza
                {
                    Id = 2,
                    Name = "4 saisons"
                },
                new Pizza
                {
                    Id = 3,
                    Name = "the best"
                }
           );
        }
    }
}