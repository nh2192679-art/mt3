using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Required, StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
