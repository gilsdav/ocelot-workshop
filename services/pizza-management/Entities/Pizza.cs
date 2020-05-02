using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaGraphQL.Entities
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public ICollection<PizzaTopping> PizzaToppings { get; set; }
    }
}