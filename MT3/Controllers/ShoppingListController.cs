using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MT3.Models;
using MT3.Services;

namespace MT3.Controllers
{
    [Authorize]
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListService _shoppingListService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingListController(IShoppingListService shoppingListService, UserManager<ApplicationUser> userManager)
        {
            _shoppingListService = shoppingListService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User)!;
            var vm = await _shoppingListService.GetShoppingListAsync(userId);
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFromRecipe(int recipeId)
        {
            var userId = _userManager.GetUserId(User)!;
            await _shoppingListService.AddFromRecipeAsync(userId, recipeId);
            TempData["Success"] = "Ingredients added to shopping list!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int itemId)
        {
            var userId = _userManager.GetUserId(User)!;
            await _shoppingListService.ToggleItemAsync(itemId, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int itemId)
        {
            var userId = _userManager.GetUserId(User)!;
            await _shoppingListService.RemoveItemAsync(itemId, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var userId = _userManager.GetUserId(User)!;
            await _shoppingListService.ClearListAsync(userId);
            TempData["Success"] = "Shopping list cleared.";
            return RedirectToAction(nameof(Index));
        }
    }
}
