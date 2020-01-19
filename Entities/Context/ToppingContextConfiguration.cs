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
                    Name = "gorgonzola"
                },
                new Topping
                {
                    Id = 2,
                    Name = "salami"
                },
                new Topping
                {
                    Id = 3,
                    Name = "thon"
                }
           );
        }
    }
}