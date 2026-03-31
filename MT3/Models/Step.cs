using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class Step
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public int StepNumber { get; set; }

        [Required]
        public string Instruction { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
    }
}
