using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MT3.Models;
using MT3.Models.ViewModels;
using MT3.Services;

namespace MT3.Controllers
{
    [Authorize]
    public class MealPlanController : Controller
    {
        private readonly IMealPlanService _mealPlanService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MealPlanController(IMealPlanService mealPlanService, UserManager<ApplicationUser> userManager)
        {
            _mealPlanService = mealPlanService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? weekStart)
        {
            var userId = _userManager.GetUserId(User)!;
            DateTime start;
            if (!DateTime.TryParse(weekStart, out start))
            {
                var today = DateTime.Today;
                start = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                if (today.DayOfWeek == DayOfWeek.Sunday) start = start.AddDays(-7);
            }

            var vm = await _mealPlanService.GetWeeklyPlanAsync(userId, start);
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(DateTime date, string mealType, int recipeId, string? weekStart)
        {
            var userId = _userManager.GetUserId(User)!;
            await _mealPlanService.AddMealAsync(userId, date, mealType, recipeId);
            TempData["Success"] = "Meal added to plan!";
            return RedirectToAction(nameof(Index), new { weekStart });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMeal(int mealPlanId, string? weekStart)
        {
            var userId = _userManager.GetUserId(User)!;
            await _mealPlanService.RemoveMealAsync(mealPlanId, userId);
            return RedirectToAction(nameof(Index), new { weekStart });
        }
    }
}
