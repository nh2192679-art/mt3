using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MT3.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Display(Name = "Cooking Time (min)")]
        public int CookingTime { get; set; }

        public int Calories { get; set; }

        [Display(Name = "Difficulty")]
        public string Difficulty { get; set; } = "Easy";

        public int Servings { get; set; } = 2;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; } = true;

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<Step> Steps { get; set; } = new List<Step>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();

        [NotMapped]
        public double AverageRating => Ratings.Any() ? Ratings.Average(r => r.Score) : 0;

        [NotMapped]
        public int RatingCount => Ratings.Count;
    }
}
