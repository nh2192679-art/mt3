using Microsoft.EntityFrameworkCore;
using MT3.Data;
using MT3.Models;
using MT3.Models.ViewModels;

namespace MT3.Services
{
    public interface IMealPlanService
    {
        Task<MealPlanViewModel> GetWeeklyPlanAsync(string userId, DateTime weekStart);
        Task AddMealAsync(string userId, DateTime date, string mealType, int recipeId);
        Task RemoveMealAsync(int mealPlanId, string userId);
    }

    public class MealPlanService : IMealPlanService
    {
        private readonly ApplicationDbContext _context;

        public MealPlanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MealPlanViewModel> GetWeeklyPlanAsync(string userId, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(7);
            var plans = await _context.MealPlans
                .Include(m => m.Recipe).ThenInclude(r => r!.Category)
                .Where(m => m.UserId == userId && m.Date >= weekStart && m.Date < weekEnd)
                .OrderBy(m => m.Date)
                .ToListAsync();

            var allRecipes = await _context.Recipes
                .Include(r => r.Category)
                .Where(r => r.IsPublished)
                .ToListAsync();

            var planByDay = new Dictionary<string, List<MealPlan>>();
            for (int i = 0; i < 7; i++)
            {
                var day = weekStart.AddDays(i).ToString("yyyy-MM-dd");
                planByDay[day] = plans.Where(p => p.Date.Date == weekStart.AddDays(i).Date).ToList();
            }

            return new MealPlanViewModel
            {
                WeekStart = weekStart,
                MealPlans = plans,
                AllRecipes = allRecipes,
                PlanByDay = planByDay
            };
        }

        public async Task AddMealAsync(string userId, DateTime date, string mealType, int recipeId)
        {
            if (recipeId <= 0) return;

            var recipeExists = await _context.Recipes.AnyAsync(r => r.Id == recipeId);
            if (!recipeExists) return;

            // Không thêm trùng cùng món trong cùng bữa ăn của cùng ngày
            var alreadyExists = await _context.MealPlans.AnyAsync(m =>
                m.UserId == userId &&
                m.Date == date.Date &&
                m.MealType == mealType &&
                m.RecipeId == recipeId);
            if (alreadyExists) return;

            _context.MealPlans.Add(new MealPlan
            {
                UserId = userId,
                Date = date.Date,
                MealType = mealType,
                RecipeId = recipeId
            });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMealAsync(int mealPlanId, string userId)
        {
            var plan = await _context.MealPlans.FirstOrDefaultAsync(m => m.Id == mealPlanId && m.UserId == userId);
            if (plan != null)
            {
                _context.MealPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IShoppingListService
    {
        Task<ShoppingListViewModel> GetShoppingListAsync(string userId);
        Task AddFromRecipeAsync(string userId, int recipeId);
        Task RemoveItemAsync(int itemId, string userId);
        Task ToggleItemAsync(int itemId, string userId);
        Task ClearListAsync(string userId);
    }

    public class ShoppingListService : IShoppingListService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingListViewModel> GetShoppingListAsync(string userId)
        {
            var items = await _context.ShoppingLists
                .Include(s => s.Ingredient)
                .Where(s => s.UserId == userId)
                .OrderBy(s => s.IsChecked).ThenBy(s => s.Ingredient!.Name)
                .ToListAsync();

            var userRecipes = await _context.Recipes
                .Include(r => r.Category)
                .Where(r => r.IsPublished)
                .Take(20)
                .ToListAsync();

            return new ShoppingListViewModel { Items = items, UserRecipes = userRecipes };
        }

        public async Task AddFromRecipeAsync(string userId, int recipeId)
        {
            var ingredients = await _context.RecipeIngredients
                .Include(ri => ri.Ingredient)
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();

            foreach (var ri in ingredients)
            {
                var exists = await _context.ShoppingLists.AnyAsync(s => s.UserId == userId && s.IngredientId == ri.IngredientId);
                if (!exists)
                {
                    _context.ShoppingLists.Add(new ShoppingList
                    {
                        UserId = userId,
                        IngredientId = ri.IngredientId,
                        Quantity = ri.Quantity
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int itemId, string userId)
        {
            var item = await _context.ShoppingLists.FirstOrDefaultAsync(s => s.Id == itemId && s.UserId == userId);
            if (item != null)
            {
                _context.ShoppingLists.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleItemAsync(int itemId, string userId)
        {
            var item = await _context.ShoppingLists.FirstOrDefaultAsync(s => s.Id == itemId && s.UserId == userId);
            if (item != null)
            {
                item.IsChecked = !item.IsChecked;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearListAsync(string userId)
        {
            var items = _context.ShoppingLists.Where(s => s.UserId == userId);
            _context.ShoppingLists.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
