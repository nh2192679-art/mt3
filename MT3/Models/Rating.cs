using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }
    }
}
