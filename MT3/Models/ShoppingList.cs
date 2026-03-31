using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Required, StringLength(100)]
        public string Quantity { get; set; } = string.Empty;

        public bool IsChecked { get; set; } = false;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
