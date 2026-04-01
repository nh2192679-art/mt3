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
                    new Category { Name = "Breakfast", IconClass = "bi-sunrise" },
                    new Category { Name = "Lunch",     IconClass = "bi-sun" },
                    new Category { Name = "Dinner",    IconClass = "bi-moon-stars" },
                    new Category { Name = "Vegan",     IconClass = "bi-flower1" },
                    new Category { Name = "Drinks",    IconClass = "bi-cup-straw" }
                );
                await context.SaveChangesAsync();
            }

            // Lấy Id thực sau khi insert
            var catBreakfast = await context.Categories.FirstAsync(c => c.Name == "Breakfast");
            var catLunch     = await context.Categories.FirstAsync(c => c.Name == "Lunch");
            var catDinner    = await context.Categories.FirstAsync(c => c.Name == "Dinner");
            var catVegan     = await context.Categories.FirstAsync(c => c.Name == "Vegan");
            var catDrinks    = await context.Categories.FirstAsync(c => c.Name == "Drinks");

            // ── INGREDIENTS ───────────────────────────────────────────────────
            if (!await context.Ingredients.AnyAsync())
            {
                context.Ingredients.AddRange(
                    new Ingredient { Name = "All-purpose flour", Unit = "cup" },
                    new Ingredient { Name = "Eggs",              Unit = "pcs" },
                    new Ingredient { Name = "Milk",              Unit = "ml" },
                    new Ingredient { Name = "Butter",            Unit = "tbsp" },
                    new Ingredient { Name = "Sugar",             Unit = "tbsp" },
                    new Ingredient { Name = "Salt",              Unit = "tsp" },
                    new Ingredient { Name = "Chicken breast",    Unit = "g" },
                    new Ingredient { Name = "Garlic",            Unit = "cloves" },
                    new Ingredient { Name = "Onion",             Unit = "pcs" },
                    new Ingredient { Name = "Olive oil",         Unit = "tbsp" },
                    new Ingredient { Name = "Tomatoes",          Unit = "pcs" },
                    new Ingredient { Name = "Bell pepper",       Unit = "pcs" },
                    new Ingredient { Name = "Pasta",             Unit = "g" },
                    new Ingredient { Name = "Parmesan cheese",   Unit = "g" },
                    new Ingredient { Name = "Fresh basil",       Unit = "leaves" },
                    new Ingredient { Name = "Lemon",             Unit = "pcs" },
                    new Ingredient { Name = "Honey",             Unit = "tbsp" },
                    new Ingredient { Name = "Greek yogurt",      Unit = "cup" },
                    new Ingredient { Name = "Oats",              Unit = "cup" },
                    new Ingredient { Name = "Berries",           Unit = "cup" }
                );
                await context.SaveChangesAsync();
            }

            // Lấy Id thực sau khi insert
            var ingEggs      = await context.Ingredients.FirstAsync(i => i.Name == "Eggs");
            var ingMilk      = await context.Ingredients.FirstAsync(i => i.Name == "Milk");
            var ingButter    = await context.Ingredients.FirstAsync(i => i.Name == "Butter");
            var ingSalt      = await context.Ingredients.FirstAsync(i => i.Name == "Salt");
            var ingChicken   = await context.Ingredients.FirstAsync(i => i.Name == "Chicken breast");
            var ingGarlic    = await context.Ingredients.FirstAsync(i => i.Name == "Garlic");
            var ingOliveOil  = await context.Ingredients.FirstAsync(i => i.Name == "Olive oil");
            var ingTomatoes  = await context.Ingredients.FirstAsync(i => i.Name == "Tomatoes");
            var ingPasta     = await context.Ingredients.FirstAsync(i => i.Name == "Pasta");
            var ingParmesan  = await context.Ingredients.FirstAsync(i => i.Name == "Parmesan cheese");
            var ingHoney     = await context.Ingredients.FirstAsync(i => i.Name == "Honey");
            var ingYogurt    = await context.Ingredients.FirstAsync(i => i.Name == "Greek yogurt");
            var ingOats      = await context.Ingredients.FirstAsync(i => i.Name == "Oats");
            var ingBerries   = await context.Ingredients.FirstAsync(i => i.Name == "Berries");

            // ── RECIPES ───────────────────────────────────────────────────────
            if (!await context.Recipes.AnyAsync())
            {
                var r1  = new Recipe { Title = "Classic French Omelette",         Description = "A perfectly cooked French omelette with a silky texture, golden outside and soft inside. Perfect for a quick breakfast.", ImageUrl = "https://images.unsplash.com/photo-1510693206972-df098062cb71?w=600",   CookingTime = 10, Calories = 320, Difficulty = "Easy",   Servings = 1, CategoryId = catBreakfast.Id, IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r2  = new Recipe { Title = "Avocado Toast with Poached Egg",   Description = "Creamy avocado on crispy toast topped with a perfectly poached egg. A trendy and nutritious breakfast.",                 ImageUrl = "https://images.unsplash.com/photo-1525351484163-7529414344d8?w=600", CookingTime = 15, Calories = 380, Difficulty = "Easy",   Servings = 2, CategoryId = catBreakfast.Id, IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r3  = new Recipe { Title = "Grilled Chicken Caesar Salad",     Description = "Tender grilled chicken over crisp romaine lettuce with creamy Caesar dressing and crunchy croutons.",                    ImageUrl = "https://images.unsplash.com/photo-1546793665-c74683f339c1?w=600", CookingTime = 25, Calories = 450, Difficulty = "Medium", Servings = 2, CategoryId = catLunch.Id,     IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r4  = new Recipe { Title = "Spaghetti Carbonara",              Description = "Classic Italian pasta dish with creamy egg sauce, crispy pancetta and freshly grated Parmesan cheese.",                  ImageUrl = "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=600", CookingTime = 30, Calories = 620, Difficulty = "Medium", Servings = 4, CategoryId = catDinner.Id,    IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r5  = new Recipe { Title = "Vegan Buddha Bowl",                Description = "A nourishing bowl packed with roasted vegetables, quinoa, chickpeas and tahini dressing.",                               ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=600", CookingTime = 35, Calories = 480, Difficulty = "Easy",   Servings = 2, CategoryId = catVegan.Id,     IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r6  = new Recipe { Title = "Mango Smoothie Bowl",              Description = "Vibrant and refreshing smoothie bowl topped with fresh fruits, granola and chia seeds.",                                 ImageUrl = "https://images.unsplash.com/photo-1590301157890-4810ed352733?w=600", CookingTime = 10, Calories = 290, Difficulty = "Easy",   Servings = 1, CategoryId = catDrinks.Id,    IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r7  = new Recipe { Title = "Beef Tacos",                       Description = "Juicy seasoned beef in crispy taco shells topped with salsa, guacamole and sour cream.",                                 ImageUrl = "https://images.unsplash.com/photo-1551504734-5ee1c4a1479b?w=600",   CookingTime = 40, Calories = 580, Difficulty = "Medium", Servings = 4, CategoryId = catDinner.Id,    IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r8  = new Recipe { Title = "Greek Yogurt Parfait",             Description = "Layers of creamy Greek yogurt, fresh berries and crunchy granola for a healthy breakfast.",                              ImageUrl = "https://images.unsplash.com/photo-1488477181946-6428a0291777?w=600", CookingTime =  5, Calories = 250, Difficulty = "Easy",   Servings = 1, CategoryId = catBreakfast.Id, IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r9  = new Recipe { Title = "Lemon Garlic Salmon",              Description = "Pan-seared salmon fillet with zesty lemon garlic butter sauce, served with roasted asparagus.",                          ImageUrl = "https://images.unsplash.com/photo-1467003909585-2f8a72700288?w=600", CookingTime = 20, Calories = 520, Difficulty = "Medium", Servings = 2, CategoryId = catDinner.Id,    IsPublished = true, CreatedAt = DateTime.UtcNow };
                var r10 = new Recipe { Title = "Overnight Oats",                   Description = "Prep-ahead breakfast with rolled oats soaked in milk, topped with fruits and honey.",                                    ImageUrl = "https://images.immediate.co.uk/production/volatile/sites/30/2025/02/OvernightOats-bf5484f.jpg?resize=768,713", CookingTime = 5, Calories = 310, Difficulty = "Easy", Servings = 1, CategoryId = catBreakfast.Id, IsPublished = true, CreatedAt = DateTime.UtcNow };

                context.Recipes.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);
                await context.SaveChangesAsync();

                // ── RECIPE INGREDIENTS ────────────────────────────────────────
                context.RecipeIngredients.AddRange(
                    new RecipeIngredient { RecipeId = r1.Id,  IngredientId = ingEggs.Id,     Quantity = "3 large" },
                    new RecipeIngredient { RecipeId = r1.Id,  IngredientId = ingButter.Id,   Quantity = "1 tbsp" },
                    new RecipeIngredient { RecipeId = r1.Id,  IngredientId = ingSalt.Id,     Quantity = "1/4 tsp" },
                    new RecipeIngredient { RecipeId = r2.Id,  IngredientId = ingEggs.Id,     Quantity = "2 large" },
                    new RecipeIngredient { RecipeId = r2.Id,  IngredientId = ingOliveOil.Id, Quantity = "1 tbsp" },
                    new RecipeIngredient { RecipeId = r3.Id,  IngredientId = ingChicken.Id,  Quantity = "300g" },
                    new RecipeIngredient { RecipeId = r3.Id,  IngredientId = ingGarlic.Id,   Quantity = "2 cloves" },
                    new RecipeIngredient { RecipeId = r3.Id,  IngredientId = ingOliveOil.Id, Quantity = "2 tbsp" },
                    new RecipeIngredient { RecipeId = r4.Id,  IngredientId = ingEggs.Id,     Quantity = "3 large" },
                    new RecipeIngredient { RecipeId = r4.Id,  IngredientId = ingPasta.Id,    Quantity = "400g" },
                    new RecipeIngredient { RecipeId = r4.Id,  IngredientId = ingParmesan.Id, Quantity = "100g" },
                    new RecipeIngredient { RecipeId = r5.Id,  IngredientId = ingOliveOil.Id, Quantity = "2 tbsp" },
                    new RecipeIngredient { RecipeId = r5.Id,  IngredientId = ingTomatoes.Id, Quantity = "2 cups roasted" },
                    new RecipeIngredient { RecipeId = r8.Id,  IngredientId = ingYogurt.Id,   Quantity = "1 cup" },
                    new RecipeIngredient { RecipeId = r8.Id,  IngredientId = ingBerries.Id,  Quantity = "1/2 cup" },
                    new RecipeIngredient { RecipeId = r10.Id, IngredientId = ingMilk.Id,     Quantity = "1/2 cup" },
                    new RecipeIngredient { RecipeId = r10.Id, IngredientId = ingHoney.Id,    Quantity = "1 tbsp" },
                    new RecipeIngredient { RecipeId = r10.Id, IngredientId = ingOats.Id,     Quantity = "1/2 cup" }
                );

                // ── STEPS ─────────────────────────────────────────────────────
                context.Steps.AddRange(
                    new Step { RecipeId = r1.Id,  StepNumber = 1, Instruction = "Crack the eggs into a bowl, add a pinch of salt and whisk until well combined." },
                    new Step { RecipeId = r1.Id,  StepNumber = 2, Instruction = "Heat butter in a non-stick pan over medium-high heat until foaming." },
                    new Step { RecipeId = r1.Id,  StepNumber = 3, Instruction = "Pour in the eggs and immediately stir with a spatula while shaking the pan." },
                    new Step { RecipeId = r1.Id,  StepNumber = 4, Instruction = "When eggs are just set, fold the omelette and slide onto a plate. Serve immediately." },
                    new Step { RecipeId = r4.Id,  StepNumber = 1, Instruction = "Cook pasta in salted boiling water until al dente. Reserve 1 cup pasta water." },
                    new Step { RecipeId = r4.Id,  StepNumber = 2, Instruction = "In a bowl, whisk together eggs, grated Parmesan, and black pepper." },
                    new Step { RecipeId = r4.Id,  StepNumber = 3, Instruction = "Fry pancetta until crispy. Remove pan from heat." },
                    new Step { RecipeId = r4.Id,  StepNumber = 4, Instruction = "Add hot pasta to the pancetta pan. Pour egg mixture and toss quickly, adding pasta water as needed." },
                    new Step { RecipeId = r4.Id,  StepNumber = 5, Instruction = "Serve immediately with extra Parmesan and black pepper." },
                    new Step { RecipeId = r9.Id,  StepNumber = 1, Instruction = "Pat salmon dry and season with salt and pepper." },
                    new Step { RecipeId = r9.Id,  StepNumber = 2, Instruction = "Heat olive oil in pan over medium-high heat. Cook salmon skin-side up for 4 minutes." },
                    new Step { RecipeId = r9.Id,  StepNumber = 3, Instruction = "Flip and cook 3 more minutes. Add butter, garlic and lemon juice." },
                    new Step { RecipeId = r9.Id,  StepNumber = 4, Instruction = "Baste salmon with butter sauce and serve with roasted asparagus." },
                    new Step { RecipeId = r10.Id, StepNumber = 1, Instruction = "Combine oats and milk in a jar or container. Stir in honey." },
                    new Step { RecipeId = r10.Id, StepNumber = 2, Instruction = "Cover and refrigerate overnight (at least 6 hours)." },
                    new Step { RecipeId = r10.Id, StepNumber = 3, Instruction = "In the morning, top with fresh fruits and a drizzle of honey. Enjoy cold!" }
                );
                await context.SaveChangesAsync();
            }

            // ── ADMIN USER ────────────────────────────────────────────────────
            if (await userManager.FindByEmailAsync("admin@chefvn.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName       = "admin@chefvn.com",
                    Email          = "admin@chefvn.com",
                    FullName       = "Administrator",
                    EmailConfirmed = true,
                    JoinedAt       = DateTime.UtcNow,
                    AvatarUrl      = "https://ui-avatars.com/api/?name=Admin&background=e84118&color=fff"
                };
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // ── DEMO USER ─────────────────────────────────────────────────────
            if (await userManager.FindByEmailAsync("chef@chefvn.com") == null)
            {
                var chef = new ApplicationUser
                {
                    UserName       = "chef@chefvn.com",
                    Email          = "chef@chefvn.com",
                    FullName       = "Demo Chef",
                    EmailConfirmed = true,
                    JoinedAt       = DateTime.UtcNow,
                    AvatarUrl      = "https://ui-avatars.com/api/?name=Chef&background=ff6b35&color=fff"
                };
                var result = await userManager.CreateAsync(chef, "Chef@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(chef, "User");
            }
        }
    }
}

