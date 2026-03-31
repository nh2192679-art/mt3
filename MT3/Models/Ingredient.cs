using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Unit { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
    }
}
