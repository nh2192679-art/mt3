using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MT3.Data;
using MT3.Models.ViewModels;
using MT3.Services;

namespace MT3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ApplicationDbContext _context;

        public HomeController(IRecipeService recipeService, ApplicationDbContext context)
        {
            _recipeService = recipeService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featured = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .Where(r => r.IsPublished)
                .OrderByDescending(r => r.CreatedAt)
                .Take(6)
                .ToListAsync();

            var trending = await _recipeService.GetTrendingRecipesAsync(5);
            var categories = await _context.Categories.ToListAsync();

            var vm = new HomeViewModel
            {
                FeaturedRecipes = featured,
                TrendingRecipes = trending,
                Categories = categories
            };
            return View(vm);
        }

        public async Task<IActionResult> Search(string q)
        {
            return RedirectToAction("Index", "Recipe", new { search = q });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new MT3.Models.ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

