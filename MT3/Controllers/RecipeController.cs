using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MT3.Data;
using MT3.Models;
using MT3.Models.ViewModels;
using MT3.Services;

namespace MT3.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUpload;

        public RecipeController(IRecipeService recipeService, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context, IFileUploadService fileUpload)
        {
            _recipeService = recipeService;
            _userManager = userManager;
            _context = context;
            _fileUpload = fileUpload;
        }

        public async Task<IActionResult> Index(string? search, int? categoryId, string? ingredient, int page = 1)
        {
            var vm = await _recipeService.GetRecipesAsync(search, categoryId, ingredient, page, 9);
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var vm = await _recipeService.GetRecipeDetailAsync(id, userId);
            if (vm.Recipe == null) return NotFound();
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateRecipeViewModel
            {
                Categories = await _context.Categories.ToListAsync(),
                AllIngredients = await _context.Ingredients.OrderBy(i => i.Name).ToListAsync()
            };
            return View(vm);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRecipeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _context.Categories.ToListAsync();
                vm.AllIngredients = await _context.Ingredients.OrderBy(i => i.Name).ToListAsync();
                return View(vm);
            }

            var userId = _userManager.GetUserId(User);
            var imageUrl = await _fileUpload.UploadImageAsync(vm.ImageFile);

            vm.Recipe.UserId = userId;
            vm.Recipe.ImageUrl = imageUrl ?? vm.Recipe.ImageUrl;
            vm.Recipe.CreatedAt = DateTime.UtcNow;

            var id = await _recipeService.CreateRecipeAsync(vm.Recipe, vm.IngredientInputs, vm.StepInputs);
            TempData["Success"] = "Recipe created successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeService.GetRecipeForEditAsync(id);
            if (recipe == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            if (recipe.UserId != userId && !User.IsInRole("Admin")) return Forbid();

            var vm = new CreateRecipeViewModel
            {
                Recipe = recipe,
                Categories = await _context.Categories.ToListAsync(),
                AllIngredients = await _context.Ingredients.OrderBy(i => i.Name).ToListAsync(),
                IngredientInputs = recipe.RecipeIngredients.Select(ri => new RecipeIngredientInput
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient?.Name ?? "",
                    Quantity = ri.Quantity
                }).ToList(),
                StepInputs = recipe.Steps.Select(s => new StepInput
                {
                    StepNumber = s.StepNumber,
                    Instruction = s.Instruction
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateRecipeViewModel vm)
        {
            if (id != vm.Recipe.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                vm.Categories = await _context.Categories.ToListAsync();
                vm.AllIngredients = await _context.Ingredients.OrderBy(i => i.Name).ToListAsync();
                return View(vm);
            }

            var userId = _userManager.GetUserId(User);
            var existing = await _context.Recipes.FindAsync(id);
            if (existing == null) return NotFound();
            if (existing.UserId != userId && !User.IsInRole("Admin")) return Forbid();

            var imageUrl = await _fileUpload.UploadImageAsync(vm.ImageFile);
            if (imageUrl != null) vm.Recipe.ImageUrl = imageUrl;

            await _recipeService.UpdateRecipeAsync(vm.Recipe, vm.IngredientInputs, vm.StepInputs);
            TempData["Success"] = "Recipe updated successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();
            if (recipe.UserId != userId && !User.IsInRole("Admin")) return Forbid();

            await _recipeService.DeleteRecipeAsync(id);
            TempData["Success"] = "Recipe deleted.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Rate(int recipeId, int score)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            await _recipeService.RateRecipeAsync(recipeId, userId, score);
            return RedirectToAction(nameof(Details), new { id = recipeId });
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> ToggleFavorite(int recipeId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            await _recipeService.ToggleFavoriteAsync(recipeId, userId);
            return RedirectToAction(nameof(Details), new { id = recipeId });
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int recipeId, string content)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            if (!string.IsNullOrWhiteSpace(content))
                await _recipeService.AddCommentAsync(recipeId, userId, content);
            return RedirectToAction(nameof(Details), new { id = recipeId });
        }

        [Authorize]
        public async Task<IActionResult> MyRecipes()
        {
            var userId = _userManager.GetUserId(User);
            var recipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return View(recipes);
        }

        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _context.Favorites
                .Include(f => f.Recipe).ThenInclude(r => r!.Category)
                .Include(f => f.Recipe).ThenInclude(r => r!.Ratings)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.SavedAt)
                .ToListAsync();
            return View(favorites);
        }
    }
}
