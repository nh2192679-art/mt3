using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MT3.Models;

namespace MT3.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Đảm bảo database đã được migrate
            await context.Database.MigrateAsync();

            // ── ROLES ──────────────────────────────────────────────────────────
            foreach (var role in new[] { "Admin", "User" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ── CATEGORIES ────────────────────────────────────────────────────
            if (!await context.Categories.AnyAsync())
            {
                context.Categories.AddRange(
                    new Category { Id = 1, Name = "Breakfast", IconClass = "bi-sunrise" },
                    new Category { Id = 2, Name = "Lunch",     IconClass = "bi-sun" },
                    new Category { Id = 3, Name = "Dinner",    IconClass = "bi-moon-stars" },
                    new Category { Id = 4, Name = "Vegan",     IconClass = "bi-flower1" },
                    new Category { Id = 5, Name = "Drinks",    IconClass = "bi-cup-straw" }
                );
                await context.SaveChangesAsync();
            }

            // ── INGREDIENTS ───────────────────────────────────────────────────
            if (!await context.Ingredients.AnyAsync())
            {
                context.Ingredients.AddRange(
                    new Ingredient { Id = 1,  Name = "All-purpose flour", Unit = "cup" },
                    new Ingredient { Id = 2,  Name = "Eggs",              Unit = "pcs" },
                    new Ingredient { Id = 3,  Name = "Milk",              Unit = "ml" },
                    new Ingredient { Id = 4,  Name = "Butter",            Unit = "tbsp" },
                    new Ingredient { Id = 5,  Name = "Sugar",             Unit = "tbsp" },
                    new Ingredient { Id = 6,  Name = "Salt",              Unit = "tsp" },
                    new Ingredient { Id = 7,  Name = "Chicken breast",    Unit = "g" },
                    new Ingredient { Id = 8,  Name = "Garlic",            Unit = "cloves" },
                    new Ingredient { Id = 9,  Name = "Onion",             Unit = "pcs" },
                    new Ingredient { Id = 10, Name = "Olive oil",         Unit = "tbsp" },
                    new Ingredient { Id = 11, Name = "Tomatoes",          Unit = "pcs" },
                    new Ingredient { Id = 12, Name = "Bell pepper",       Unit = "pcs" },
                    new Ingredient { Id = 13, Name = "Pasta",             Unit = "g" },
                    new Ingredient { Id = 14, Name = "Parmesan cheese",   Unit = "g" },
                    new Ingredient { Id = 15, Name = "Fresh basil",       Unit = "leaves" },
                    new Ingredient { Id = 16, Name = "Lemon",             Unit = "pcs" },
                    new Ingredient { Id = 17, Name = "Honey",             Unit = "tbsp" },
                    new Ingredient { Id = 18, Name = "Greek yogurt",      Unit = "cup" },
                    new Ingredient { Id = 19, Name = "Oats",              Unit = "cup" },
                    new Ingredient { Id = 20, Name = "Berries",           Unit = "cup" }
                );
                await context.SaveChangesAsync();
            }

            // ── RECIPES ───────────────────────────────────────────────────────
            if (!await context.Recipes.AnyAsync())
            {
                var recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Id = 1, Title = "Classic French Omelette",
                        Description = "A perfectly cooked French omelette with a silky texture, golden outside and soft inside. Perfect for a quick breakfast.",
                        ImageUrl = "https://images.unsplash.com/photo-1510693206972-df098062cb71?w=600",
                        CookingTime = 10, Calories = 320, Difficulty = "Easy", Servings = 1,
                        CategoryId = 1, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 0, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 2, Title = "Avocado Toast with Poached Egg",
                        Description = "Creamy avocado on crispy toast topped with a perfectly poached egg. A trendy and nutritious breakfast.",
                        ImageUrl = "https://images.unsplash.com/photo-1525351484163-7529414344d8?w=600",
                        CookingTime = 15, Calories = 380, Difficulty = "Easy", Servings = 2,
                        CategoryId = 1, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 1, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 3, Title = "Grilled Chicken Caesar Salad",
                        Description = "Tender grilled chicken over crisp romaine lettuce with creamy Caesar dressing and crunchy croutons.",
                        ImageUrl = "https://images.unsplash.com/photo-1546793665-c74683f339c1?w=600",
                        CookingTime = 25, Calories = 450, Difficulty = "Medium", Servings = 2,
                        CategoryId = 2, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 2, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 4, Title = "Spaghetti Carbonara",
                        Description = "Classic Italian pasta dish with creamy egg sauce, crispy pancetta and freshly grated Parmesan cheese.",
                        ImageUrl = "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=600",
                        CookingTime = 30, Calories = 620, Difficulty = "Medium", Servings = 4,
                        CategoryId = 3, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 3, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 5, Title = "Vegan Buddha Bowl",
                        Description = "A nourishing bowl packed with roasted vegetables, quinoa, chickpeas and tahini dressing.",
                        ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=600",
                        CookingTime = 35, Calories = 480, Difficulty = "Easy", Servings = 2,
                        CategoryId = 4, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 4, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 6, Title = "Mango Smoothie Bowl",
                        Description = "Vibrant and refreshing smoothie bowl topped with fresh fruits, granola and chia seeds.",
                        ImageUrl = "https://images.unsplash.com/photo-1590301157890-4810ed352733?w=600",
                        CookingTime = 10, Calories = 290, Difficulty = "Easy", Servings = 1,
                        CategoryId = 5, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 5, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 7, Title = "Beef Tacos",
                        Description = "Juicy seasoned beef in crispy taco shells topped with salsa, guacamole and sour cream.",
                        ImageUrl = "https://images.unsplash.com/photo-1551504734-5ee1c4a1479b?w=600",
                        CookingTime = 40, Calories = 580, Difficulty = "Medium", Servings = 4,
                        CategoryId = 3, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 6, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 8, Title = "Greek Yogurt Parfait",
                        Description = "Layers of creamy Greek yogurt, fresh berries and crunchy granola for a healthy breakfast.",
                        ImageUrl = "https://images.unsplash.com/photo-1488477181946-6428a0291777?w=600",
                        CookingTime = 5, Calories = 250, Difficulty = "Easy", Servings = 1,
                        CategoryId = 1, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 7, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 9, Title = "Lemon Garlic Salmon",
                        Description = "Pan-seared salmon fillet with zesty lemon garlic butter sauce, served with roasted asparagus.",
                        ImageUrl = "https://images.unsplash.com/photo-1467003909585-2f8a72700288?w=600",
                        CookingTime = 20, Calories = 520, Difficulty = "Medium", Servings = 2,
                        CategoryId = 3, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 8, 0, DateTimeKind.Utc)
                    },
                    new Recipe
                    {
                        Id = 10, Title = "Overnight Oats",
                        Description = "Prep-ahead breakfast with rolled oats soaked in milk, topped with fruits and honey.",
                        ImageUrl = "https://images.immediate.co.uk/production/volatile/sites/30/2025/02/OvernightOats-bf5484f.jpg?resize=768,713",
                        CookingTime = 5, Calories = 310, Difficulty = "Easy", Servings = 1,
                        CategoryId = 1, IsPublished = true, CreatedAt = new DateTime(2026, 4, 1, 20, 9, 0, DateTimeKind.Utc)
                    }
                };
                context.Recipes.AddRange(recipes);
                await context.SaveChangesAsync();

                // ── RECIPE INGREDIENTS ────────────────────────────────────────
                context.RecipeIngredients.AddRange(
                    new RecipeIngredient { RecipeId = 1,  IngredientId = 2,  Quantity = "3 large" },
                    new RecipeIngredient { RecipeId = 1,  IngredientId = 4,  Quantity = "1 tbsp" },
                    new RecipeIngredient { RecipeId = 1,  IngredientId = 6,  Quantity = "1/4 tsp" },
                    new RecipeIngredient { RecipeId = 2,  IngredientId = 2,  Quantity = "2 large" },
                    new RecipeIngredient { RecipeId = 2,  IngredientId = 10, Quantity = "1 tbsp" },
                    new RecipeIngredient { RecipeId = 3,  IngredientId = 7,  Quantity = "300g" },
                    new RecipeIngredient { RecipeId = 3,  IngredientId = 8,  Quantity = "2 cloves" },
                    new RecipeIngredient { RecipeId = 3,  IngredientId = 10, Quantity = "2 tbsp" },
                    new RecipeIngredient { RecipeId = 4,  IngredientId = 2,  Quantity = "3 large" },
                    new RecipeIngredient { RecipeId = 4,  IngredientId = 13, Quantity = "400g" },
                    new RecipeIngredient { RecipeId = 4,  IngredientId = 14, Quantity = "100g" },
                    new RecipeIngredient { RecipeId = 5,  IngredientId = 10, Quantity = "2 tbsp" },
                    new RecipeIngredient { RecipeId = 5,  IngredientId = 11, Quantity = "2 cups roasted" },
                    new RecipeIngredient { RecipeId = 8,  IngredientId = 18, Quantity = "1 cup" },
                    new RecipeIngredient { RecipeId = 8,  IngredientId = 20, Quantity = "1/2 cup" },
                    new RecipeIngredient { RecipeId = 10, IngredientId = 3,  Quantity = "1/2 cup" },
                    new RecipeIngredient { RecipeId = 10, IngredientId = 17, Quantity = "1 tbsp" },
                    new RecipeIngredient { RecipeId = 10, IngredientId = 19, Quantity = "1/2 cup" }
                );

                // ── STEPS ─────────────────────────────────────────────────────
                context.Steps.AddRange(
                    new Step { RecipeId = 1, StepNumber = 1, Instruction = "Crack the eggs into a bowl, add a pinch of salt and whisk until well combined." },
                    new Step { RecipeId = 1, StepNumber = 2, Instruction = "Heat butter in a non-stick pan over medium-high heat until foaming." },
                    new Step { RecipeId = 1, StepNumber = 3, Instruction = "Pour in the eggs and immediately stir with a spatula while shaking the pan." },
                    new Step { RecipeId = 1, StepNumber = 4, Instruction = "When eggs are just set, fold the omelette and slide onto a plate. Serve immediately." },
                    new Step { RecipeId = 4, StepNumber = 1, Instruction = "Cook pasta in salted boiling water until al dente. Reserve 1 cup pasta water." },
                    new Step { RecipeId = 4, StepNumber = 2, Instruction = "In a bowl, whisk together eggs, grated Parmesan, and black pepper." },
                    new Step { RecipeId = 4, StepNumber = 3, Instruction = "Fry pancetta until crispy. Remove pan from heat." },
                    new Step { RecipeId = 4, StepNumber = 4, Instruction = "Add hot pasta to the pancetta pan. Pour egg mixture and toss quickly, adding pasta water as needed." },
                    new Step { RecipeId = 4, StepNumber = 5, Instruction = "Serve immediately with extra Parmesan and black pepper." },
                    new Step { RecipeId = 9, StepNumber = 1, Instruction = "Pat salmon dry and season with salt and pepper." },
                    new Step { RecipeId = 9, StepNumber = 2, Instruction = "Heat olive oil in pan over medium-high heat. Cook salmon skin-side up for 4 minutes." },
                    new Step { RecipeId = 9, StepNumber = 3, Instruction = "Flip and cook 3 more minutes. Add butter, garlic and lemon juice." },
                    new Step { RecipeId = 9, StepNumber = 4, Instruction = "Baste salmon with butter sauce and serve with roasted asparagus." },
                    new Step { RecipeId = 10, StepNumber = 1, Instruction = "Combine oats and milk in a jar or container. Stir in honey." },
                    new Step { RecipeId = 10, StepNumber = 2, Instruction = "Cover and refrigerate overnight (at least 6 hours)." },
                    new Step { RecipeId = 10, StepNumber = 3, Instruction = "In the morning, top with fresh fruits and a drizzle of honey. Enjoy cold!" }
                );
                await context.SaveChangesAsync();
            }

            // ── ADMIN USER ────────────────────────────────────────────────────
            // Tài khoản mặc định: admin@chefvn.com / Admin@123
            if (await userManager.FindByEmailAsync("admin@chefvn.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName    = "admin@chefvn.com",
                    Email       = "admin@chefvn.com",
                    FullName    = "Administrator",
                    EmailConfirmed = true,
                    JoinedAt    = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    AvatarUrl   = "https://ui-avatars.com/api/?name=Admin&background=e84118&color=fff"
                };
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // ── DEMO USER ─────────────────────────────────────────────────────
            // Tài khoản demo: chef@chefvn.com / Chef@123
            if (await userManager.FindByEmailAsync("chef@chefvn.com") == null)
            {
                var chef = new ApplicationUser
                {
                    UserName    = "chef@chefvn.com",
                    Email       = "chef@chefvn.com",
                    FullName    = "Demo Chef",
                    EmailConfirmed = true,
                    JoinedAt    = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    AvatarUrl   = "https://ui-avatars.com/api/?name=Chef&background=ff6b35&color=fff"
                };
                var result = await userManager.CreateAsync(chef, "Chef@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(chef, "User");
            }
        }
    }
}
