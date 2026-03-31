using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MT3.Data;
using MT3.Models;
using MT3.Models.ViewModels;

namespace MT3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AdminDashboardViewModel
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalRecipes = await _context.Recipes.CountAsync(),
                TotalComments = await _context.Comments.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(),
                RecentRecipes = await _context.Recipes
                    .Include(r => r.Category).Include(r => r.User)
                    .OrderByDescending(r => r.CreatedAt).Take(10).ToListAsync(),
                RecentUsers = await _context.Users
                    .OrderByDescending(u => u.JoinedAt).Take(10)
                    .Cast<ApplicationUser>().ToListAsync()
            };
            return View(vm);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.Cast<ApplicationUser>().OrderByDescending(u => u.JoinedAt).ToListAsync();
            return View(users);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null) await _userManager.DeleteAsync(user);
            TempData["Success"] = "User deleted.";
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Recipes()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Category).Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt).ToListAsync();
            return View(recipes);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null) { _context.Recipes.Remove(recipe); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Recipe deleted.";
            return RedirectToAction(nameof(Recipes));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> TogglePublish(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                recipe.IsPublished = !recipe.IsPublished;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Recipes));
        }

        public async Task<IActionResult> Categories()
        {
            var cats = await _context.Categories.Include(c => c.Recipes).ToListAsync();
            return View(cats);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(string name, string? iconClass)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Categories.Add(new Category { Name = name, IconClass = iconClass });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category created.";
            }
            return RedirectToAction(nameof(Categories));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null) { _context.Categories.Remove(cat); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Categories));
        }

        public async Task<IActionResult> Comments()
        {
            var comments = await _context.Comments
                .Include(c => c.User).Include(c => c.Recipe)
                .OrderByDescending(c => c.CreatedAt).ToListAsync();
            return View(comments);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var c = await _context.Comments.FindAsync(id);
            if (c != null) { _context.Comments.Remove(c); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Comments));
        }
    }
}
