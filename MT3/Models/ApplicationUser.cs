using Microsoft.AspNetCore.Identity;

namespace MT3.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
        public ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
    }
}
