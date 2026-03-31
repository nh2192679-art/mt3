namespace MT3.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Recipe> FeaturedRecipes { get; set; } = new();
        public List<Recipe> TrendingRecipes { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public string? SearchQuery { get; set; }
    }

    public class RecipeListViewModel
    {
        public List<Recipe> Recipes { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();
        public string? SearchQuery { get; set; }
        public int? CategoryId { get; set; }
        public string? IngredientFilter { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class RecipeDetailViewModel
    {
        public Recipe Recipe { get; set; } = null!;
        public List<Recipe> RelatedRecipes { get; set; } = new();
        public int? UserRating { get; set; }
        public bool IsFavorited { get; set; }
    }

    public class CreateRecipeViewModel
    {
        public Recipe Recipe { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Ingredient> AllIngredients { get; set; } = new();
        public List<RecipeIngredientInput> IngredientInputs { get; set; } = new();
        public List<StepInput> StepInputs { get; set; } = new();
        public IFormFile? ImageFile { get; set; }
    }

    public class RecipeIngredientInput
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
    }

    public class StepInput
    {
        public int StepNumber { get; set; }
        public string Instruction { get; set; } = string.Empty;
    }

    public class MealPlanViewModel
    {
        public DateTime WeekStart { get; set; }
        public List<MealPlan> MealPlans { get; set; } = new();
        public List<Recipe> AllRecipes { get; set; } = new();
        public Dictionary<string, List<MealPlan>> PlanByDay { get; set; } = new();
    }

    public class ShoppingListViewModel
    {
        public List<ShoppingList> Items { get; set; } = new();
        public List<Recipe> UserRecipes { get; set; } = new();
    }

    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalRecipes { get; set; }
        public int TotalComments { get; set; }
        public int TotalCategories { get; set; }
        public List<Recipe> RecentRecipes { get; set; } = new();
        public List<ApplicationUser> RecentUsers { get; set; } = new();
    }
}
