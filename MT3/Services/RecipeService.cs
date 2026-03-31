using Microsoft.EntityFrameworkCore;
using MT3.Data;
using MT3.Models;
using MT3.Models.ViewModels;

namespace MT3.Services
{
    public interface IRecipeService
    {
        Task<RecipeListViewModel> GetRecipesAsync(string? search, int? categoryId, string? ingredient, int page, int pageSize);
        Task<RecipeDetailViewModel> GetRecipeDetailAsync(int id, string? userId);
        Task<List<Recipe>> GetTrendingRecipesAsync(int count = 5);
        Task<List<Recipe>> GetRelatedRecipesAsync(int recipeId, int categoryId, int count = 4);
        Task<Recipe?> GetRecipeForEditAsync(int id);
        Task<int> CreateRecipeAsync(Recipe recipe, List<RecipeIngredientInput> ingredients, List<StepInput> steps);
        Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredientInput> ingredients, List<StepInput> steps);
        Task DeleteRecipeAsync(int id);
        Task<bool> RateRecipeAsync(int recipeId, string userId, int score);
        Task<bool> ToggleFavoriteAsync(int recipeId, string userId);
        Task<bool> IsFavoritedAsync(int recipeId, string userId);
        Task<int?> GetUserRatingAsync(int recipeId, string userId);
        Task AddCommentAsync(int recipeId, string userId, string content);
    }

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RecipeListViewModel> GetRecipesAsync(string? search, int? categoryId, string? ingredient, int page, int pageSize)
        {
            var query = _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Where(r => r.IsPublished)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(r => r.Title.Contains(search) || r.Description.Contains(search));

            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(ingredient))
                query = query.Where(r => r.RecipeIngredients.Any(ri => ri.Ingredient!.Name.Contains(ingredient)));

            var total = await query.CountAsync();
            var recipes = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new RecipeListViewModel
            {
                Recipes = recipes,
                Categories = await _context.Categories.ToListAsync(),
                Ingredients = await _context.Ingredients.OrderBy(i => i.Name).ToListAsync(),
                SearchQuery = search,
                CategoryId = categoryId,
                IngredientFilter = ingredient,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<RecipeDetailViewModel> GetRecipeDetailAsync(int id, string? userId)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .Include(r => r.Ratings)
                .Include(r => r.Comments).ThenInclude(c => c.User)
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Steps.OrderBy(s => s.StepNumber))
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return new RecipeDetailViewModel();

            int? userRating = null;
            bool isFavorited = false;

            if (!string.IsNullOrEmpty(userId))
            {
                var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RecipeId == id && r.UserId == userId);
                userRating = rating?.Score;
                isFavorited = await _context.Favorites.AnyAsync(f => f.RecipeId == id && f.UserId == userId);
            }

            var related = await GetRelatedRecipesAsync(id, recipe.CategoryId);

            return new RecipeDetailViewModel
            {
                Recipe = recipe,
                RelatedRecipes = related,
                UserRating = userRating,
                IsFavorited = isFavorited
            };
        }

        public async Task<List<Recipe>> GetTrendingRecipesAsync(int count = 5)
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .Where(r => r.IsPublished)
                .OrderByDescending(r => r.Ratings.Count)
                .ThenByDescending(r => r.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetRelatedRecipesAsync(int recipeId, int categoryId, int count = 4)
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .Where(r => r.CategoryId == categoryId && r.Id != recipeId && r.IsPublished)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeForEditAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Steps.OrderBy(s => s.StepNumber))
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> CreateRecipeAsync(Recipe recipe, List<RecipeIngredientInput> ingredients, List<StepInput> steps)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            foreach (var ing in ingredients.Where(i => i.IngredientId > 0))
            {
                _context.RecipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = ing.IngredientId,
                    Quantity = ing.Quantity
                });
            }

            for (int i = 0; i < steps.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(steps[i].Instruction))
                {
                    _context.Steps.Add(new Step
                    {
                        RecipeId = recipe.Id,
                        StepNumber = i + 1,
                        Instruction = steps[i].Instruction
                    });
                }
            }

            await _context.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredientInput> ingredients, List<StepInput> steps)
        {
            var existing = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            if (existing == null) return;

            existing.Title = recipe.Title;
            existing.Description = recipe.Description;
            existing.CookingTime = recipe.CookingTime;
            existing.Calories = recipe.Calories;
            existing.Difficulty = recipe.Difficulty;
            existing.Servings = recipe.Servings;
            existing.CategoryId = recipe.CategoryId;
            if (!string.IsNullOrEmpty(recipe.ImageUrl))
                existing.ImageUrl = recipe.ImageUrl;

            _context.RecipeIngredients.RemoveRange(existing.RecipeIngredients);
            _context.Steps.RemoveRange(existing.Steps);

            foreach (var ing in ingredients.Where(i => i.IngredientId > 0))
            {
                _context.RecipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = ing.IngredientId,
                    Quantity = ing.Quantity
                });
            }

            for (int i = 0; i < steps.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(steps[i].Instruction))
                {
                    _context.Steps.Add(new Step
                    {
                        RecipeId = recipe.Id,
                        StepNumber = i + 1,
                        Instruction = steps[i].Instruction
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RateRecipeAsync(int recipeId, string userId, int score)
        {
            var existing = await _context.Ratings.FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);
            if (existing != null)
            {
                existing.Score = score;
            }
            else
            {
                _context.Ratings.Add(new Rating { RecipeId = recipeId, UserId = userId, Score = score });
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleFavoriteAsync(int recipeId, string userId)
        {
            var existing = await _context.Favorites.FirstOrDefaultAsync(f => f.RecipeId == recipeId && f.UserId == userId);
            if (existing != null)
            {
                _context.Favorites.Remove(existing);
                await _context.SaveChangesAsync();
                return false;
            }
            else
            {
                _context.Favorites.Add(new Favorite { RecipeId = recipeId, UserId = userId });
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> IsFavoritedAsync(int recipeId, string userId)
        {
            return await _context.Favorites.AnyAsync(f => f.RecipeId == recipeId && f.UserId == userId);
        }

        public async Task<int?> GetUserRatingAsync(int recipeId, string userId)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);
            return rating?.Score;
        }

        public async Task AddCommentAsync(int recipeId, string userId, string content)
        {
            _context.Comments.Add(new Comment
            {
                RecipeId = recipeId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
    }
}
