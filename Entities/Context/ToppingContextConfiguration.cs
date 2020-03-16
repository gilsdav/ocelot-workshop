using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PizzaGraphQL.Entities.Context
{
    public class ToppingContextConfiguration : IEntityTypeConfiguration<Topping>
    {
        public void Configure(EntityTypeBuilder<Topping> builder)
        {
            builder
                .HasData(
                new Topping
                {
                    Id = 1,
                    Name = "bacon"
                },
                new Topping
                {
                    Id = 2,
                    Name = "pepperoni"
                },
                new Topping
                {
                    Id = 3,
                    Name = "tomato"
                }
           );
        }
    }
}