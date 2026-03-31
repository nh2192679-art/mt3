using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Required, StringLength(100)]
        public string Quantity { get; set; } = string.Empty;
    }
}
