using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MT3.Models;

namespace MT3.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            // Seed Roles
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            ApplicationUser? adminUser = null;
            if (await userManager.FindByEmailAsync("admin@chefvn.com") == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@chefvn.com",
                    Email = "admin@chefvn.com",
                    FullName = "Admin Chef",
                    EmailConfirmed = true,
                    AvatarUrl = "https://ui-avatars.com/api/?name=Admin+Chef&background=ff6b35&color=fff"
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                adminUser = await userManager.FindByEmailAsync("admin@chefvn.com");
            }

            // Seed Demo User
            ApplicationUser? demoUser = null;
            if (await userManager.FindByEmailAsync("chef@chefvn.com") == null)
            {
                demoUser = new ApplicationUser
                {
                    UserName = "chef@chefvn.com",
                    Email = "chef@chefvn.com",
                    FullName = "Chef Master",
                    EmailConfirmed = true,
                    AvatarUrl = "https://ui-avatars.com/api/?name=Chef+Master&background=28a745&color=fff",
                    Bio = "Passionate home cook sharing authentic recipes"
                };
                await userManager.CreateAsync(demoUser, "Chef@123");
                await userManager.AddToRoleAsync(demoUser, "User");
            }
            else
            {
                demoUser = await userManager.FindByEmailAsync("chef@chefvn.com");
            }

            if (context.Categories.Any()) return;

            // Seed Categories
            var categories = new List<Category>
            {
                new() { Name = "Breakfast", IconClass = "bi-sunrise" },
                new() { Name = "Lunch", IconClass = "bi-sun" },
                new() { Name = "Dinner", IconClass = "bi-moon-stars" },
                new() { Name = "Vegan", IconClass = "bi-flower1" },
                new() { Name = "Drinks", IconClass = "bi-cup-straw" }
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            // Seed Ingredients
            var ingredients = new List<Ingredient>
            {
                new() { Name = "All-purpose flour", Unit = "cup" },
                new() { Name = "Eggs", Unit = "pcs" },
                new() { Name = "Milk", Unit = "ml" },
                new() { Name = "Butter", Unit = "tbsp" },
                new() { Name = "Sugar", Unit = "tbsp" },
                new() { Name = "Salt", Unit = "tsp" },
                new() { Name = "Chicken breast", Unit = "g" },
                new() { Name = "Garlic", Unit = "cloves" },
                new() { Name = "Onion", Unit = "pcs" },
                new() { Name = "Olive oil", Unit = "tbsp" },
                new() { Name = "Tomatoes", Unit = "pcs" },
                new() { Name = "Bell pepper", Unit = "pcs" },
                new() { Name = "Pasta", Unit = "g" },
                new() { Name = "Parmesan cheese", Unit = "g" },
                new() { Name = "Fresh basil", Unit = "leaves" },
                new() { Name = "Lemon", Unit = "pcs" },
                new() { Name = "Honey", Unit = "tbsp" },
                new() { Name = "Greek yogurt", Unit = "cup" },
                new() { Name = "Oats", Unit = "cup" },
                new() { Name = "Berries", Unit = "cup" }
            };
            context.Ingredients.AddRange(ingredients);
            await context.SaveChangesAsync();

            var adminId = adminUser?.Id;
            var demoId = demoUser?.Id;

            // Seed Recipes
            var recipes = new List<Recipe>
            {
                new()
                {
                    Title = "Classic French Omelette",
                    Description = "A perfectly cooked French omelette with a silky texture, golden outside and soft inside. Perfect for a quick breakfast.",
                    ImageUrl = "https://images.unsplash.com/photo-1510693206972-df098062cb71?w=600",
                    CookingTime = 10,
                    Calories = 320,
                    Difficulty = "Easy",
                    Servings = 1,
                    CategoryId = categories[0].Id,
                    UserId = demoId,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new()
                {
                    Title = "Avocado Toast with Poached Egg",
                    Description = "Creamy avocado on crispy toast topped with a perfectly poached egg. A trendy and nutritious breakfast.",
                    ImageUrl = "https://images.unsplash.com/photo-1525351484163-7529414344d8?w=600",
                    CookingTime = 15,
                    Calories = 380,
                    Difficulty = "Easy",
                    Servings = 2,
                    CategoryId = categories[0].Id,
                    UserId = adminId,
                    CreatedAt = DateTime.UtcNow.AddDays(-9)
                },
                new()
                {
                    Title = "Grilled Chicken Caesar Salad",
                    Description = "Tender grilled chicken over crisp romaine lettuce with creamy Caesar dressing and crunchy croutons.",
                    ImageUrl = "https://images.unsplash.com/photo-1546793665-c74683f339c1?w=600",
                    CookingTime = 25,
                    Calories = 450,
                    Difficulty = "Medium",
                    Servings = 2,
                    CategoryId = categories[1].Id,
                    UserId = demoId,
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new()
                {
                    Title = "Spaghetti Carbonara",
                    Description = "Classic Italian pasta dish with creamy egg sauce, crispy pancetta and freshly grated Parmesan cheese.",
                    ImageUrl = "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=600",
                    CookingTime = 30,
                    Calories = 620,
                    Difficulty = "Medium",
                    Servings = 4,
                    CategoryId = categories[2].Id,
                    UserId = demoId,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                },
                new()
                {
                    Title = "Vegan Buddha Bowl",
                    Description = "A nourishing bowl packed with roasted vegetables, quinoa, chickpeas and tahini dressing.",
                    ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=600",
                    CookingTime = 35,
                    Calories = 480,
                    Difficulty = "Easy",
                    Servings = 2,
                    CategoryId = categories[3].Id,
                    UserId = adminId,
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                },
                new()
                {
                    Title = "Mango Smoothie Bowl",
                    Description = "Vibrant and refreshing smoothie bowl topped with fresh fruits, granola and chia seeds.",
                    ImageUrl = "https://images.unsplash.com/photo-1590301157890-4810ed352733?w=600",
                    CookingTime = 10,
                    Calories = 290,
                    Difficulty = "Easy",
                    Servings = 1,
                    CategoryId = categories[4].Id,
                    UserId = demoId,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new()
                {
                    Title = "Beef Tacos",
                    Description = "Juicy seasoned beef in crispy taco shells topped with salsa, guacamole and sour cream.",
                    ImageUrl = "https://images.unsplash.com/photo-1551504734-5ee1c4a1479b?w=600",
                    CookingTime = 40,
                    Calories = 580,
                    Difficulty = "Medium",
                    Servings = 4,
                    CategoryId = categories[2].Id,
                    UserId = adminId,
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                },
                new()
                {
                    Title = "Greek Yogurt Parfait",
                    Description = "Layers of creamy Greek yogurt, fresh berries and crunchy granola for a healthy breakfast.",
                    ImageUrl = "https://images.unsplash.com/photo-1488477181946-6428a0291777?w=600",
                    CookingTime = 5,
                    Calories = 250,
                    Difficulty = "Easy",
                    Servings = 1,
                    CategoryId = categories[0].Id,
                    UserId = demoId,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new()
                {
                    Title = "Lemon Garlic Salmon",
                    Description = "Pan-seared salmon fillet with zesty lemon garlic butter sauce, served with roasted asparagus.",
                    ImageUrl = "https://images.unsplash.com/photo-1467003909585-2f8a72700288?w=600",
                    CookingTime = 20,
                    Calories = 520,
                    Difficulty = "Medium",
                    Servings = 2,
                    CategoryId = categories[2].Id,
                    UserId = adminId,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new()
                {
                    Title = "Overnight Oats",
                    Description = "Prep-ahead breakfast with rolled oats soaked in milk, topped with fruits and honey.",
                    ImageUrl = "https://images.unsplash.com/photo-1517673132405-a56a62b18caf?w=600",
                    CookingTime = 5,
                    Calories = 310,
                    Difficulty = "Easy",
                    Servings = 1,
                    CategoryId = categories[0].Id,
                    UserId = demoId,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };
            context.Recipes.AddRange(recipes);
            await context.SaveChangesAsync();

            // Seed Recipe Ingredients
            var recipeIngredients = new List<RecipeIngredient>
            {
                new() { RecipeId = recipes[0].Id, IngredientId = ingredients[1].Id, Quantity = "3 large" },
                new() { RecipeId = recipes[0].Id, IngredientId = ingredients[3].Id, Quantity = "1 tbsp" },
                new() { RecipeId = recipes[0].Id, IngredientId = ingredients[5].Id, Quantity = "1/4 tsp" },
                new() { RecipeId = recipes[1].Id, IngredientId = ingredients[1].Id, Quantity = "2 large" },
                new() { RecipeId = recipes[1].Id, IngredientId = ingredients[9].Id, Quantity = "1 tbsp" },
                new() { RecipeId = recipes[2].Id, IngredientId = ingredients[6].Id, Quantity = "300g" },
                new() { RecipeId = recipes[2].Id, IngredientId = ingredients[7].Id, Quantity = "2 cloves" },
                new() { RecipeId = recipes[2].Id, IngredientId = ingredients[9].Id, Quantity = "2 tbsp" },
                new() { RecipeId = recipes[3].Id, IngredientId = ingredients[12].Id, Quantity = "400g" },
                new() { RecipeId = recipes[3].Id, IngredientId = ingredients[1].Id, Quantity = "3 large" },
                new() { RecipeId = recipes[3].Id, IngredientId = ingredients[13].Id, Quantity = "100g" },
                new() { RecipeId = recipes[4].Id, IngredientId = ingredients[10].Id, Quantity = "2 cups roasted" },
                new() { RecipeId = recipes[4].Id, IngredientId = ingredients[9].Id, Quantity = "2 tbsp" },
                new() { RecipeId = recipes[7].Id, IngredientId = ingredients[17].Id, Quantity = "1 cup" },
                new() { RecipeId = recipes[7].Id, IngredientId = ingredients[19].Id, Quantity = "1/2 cup" },
                new() { RecipeId = recipes[9].Id, IngredientId = ingredients[18].Id, Quantity = "1/2 cup" },
                new() { RecipeId = recipes[9].Id, IngredientId = ingredients[2].Id, Quantity = "1/2 cup" },
                new() { RecipeId = recipes[9].Id, IngredientId = ingredients[16].Id, Quantity = "1 tbsp" },
            };
            context.RecipeIngredients.AddRange(recipeIngredients);

            // Seed Steps
            var steps = new List<Step>
            {
                // French Omelette
                new() { RecipeId = recipes[0].Id, StepNumber = 1, Instruction = "Crack the eggs into a bowl, add a pinch of salt and whisk until well combined." },
                new() { RecipeId = recipes[0].Id, StepNumber = 2, Instruction = "Heat butter in a non-stick pan over medium-high heat until foaming." },
                new() { RecipeId = recipes[0].Id, StepNumber = 3, Instruction = "Pour in the eggs and immediately stir with a spatula while shaking the pan." },
                new() { RecipeId = recipes[0].Id, StepNumber = 4, Instruction = "When eggs are just set, fold the omelette and slide onto a plate. Serve immediately." },
                // Carbonara
                new() { RecipeId = recipes[3].Id, StepNumber = 1, Instruction = "Cook pasta in salted boiling water until al dente. Reserve 1 cup pasta water." },
                new() { RecipeId = recipes[3].Id, StepNumber = 2, Instruction = "In a bowl, whisk together eggs, grated Parmesan, and black pepper." },
                new() { RecipeId = recipes[3].Id, StepNumber = 3, Instruction = "Fry pancetta until crispy. Remove pan from heat." },
                new() { RecipeId = recipes[3].Id, StepNumber = 4, Instruction = "Add hot pasta to the pancetta pan. Pour egg mixture and toss quickly, adding pasta water as needed." },
                new() { RecipeId = recipes[3].Id, StepNumber = 5, Instruction = "Serve immediately with extra Parmesan and black pepper." },
                // Salmon
                new() { RecipeId = recipes[8].Id, StepNumber = 1, Instruction = "Pat salmon dry and season with salt and pepper." },
                new() { RecipeId = recipes[8].Id, StepNumber = 2, Instruction = "Heat olive oil in pan over medium-high heat. Cook salmon skin-side up for 4 minutes." },
                new() { RecipeId = recipes[8].Id, StepNumber = 3, Instruction = "Flip and cook 3 more minutes. Add butter, garlic and lemon juice." },
                new() { RecipeId = recipes[8].Id, StepNumber = 4, Instruction = "Baste salmon with butter sauce and serve with roasted asparagus." },
                // Overnight Oats
                new() { RecipeId = recipes[9].Id, StepNumber = 1, Instruction = "Combine oats and milk in a jar or container. Stir in honey." },
                new() { RecipeId = recipes[9].Id, StepNumber = 2, Instruction = "Cover and refrigerate overnight (at least 6 hours)." },
                new() { RecipeId = recipes[9].Id, StepNumber = 3, Instruction = "In the morning, top with fresh fruits and a drizzle of honey. Enjoy cold!" },
            };
            context.Steps.AddRange(steps);

            // Seed Ratings
            var ratings = new List<Rating>
            {
                new() { RecipeId = recipes[0].Id, UserId = adminId, Score = 5 },
                new() { RecipeId = recipes[1].Id, UserId = demoId, Score = 4 },
                new() { RecipeId = recipes[2].Id, UserId = adminId, Score = 5 },
                new() { RecipeId = recipes[3].Id, UserId = adminId, Score = 4 },
                new() { RecipeId = recipes[3].Id, UserId = demoId, Score = 5 },
                new() { RecipeId = recipes[4].Id, UserId = demoId, Score = 4 },
                new() { RecipeId = recipes[5].Id, UserId = adminId, Score = 5 },
                new() { RecipeId = recipes[6].Id, UserId = demoId, Score = 4 },
                new() { RecipeId = recipes[7].Id, UserId = adminId, Score = 5 },
                new() { RecipeId = recipes[8].Id, UserId = demoId, Score = 5 },
                new() { RecipeId = recipes[9].Id, UserId = adminId, Score = 4 },
            };
            context.Ratings.AddRange(ratings);

            // Seed Comments
            var comments = new List<Comment>
            {
                new() { RecipeId = recipes[0].Id, UserId = adminId, Content = "Absolutely delicious! I added some fresh herbs and it was perfect.", CreatedAt = DateTime.UtcNow.AddDays(-8) },
                new() { RecipeId = recipes[0].Id, UserId = demoId, Content = "Best omelette recipe I've tried. The technique really works!", CreatedAt = DateTime.UtcNow.AddDays(-7) },
                new() { RecipeId = recipes[3].Id, UserId = demoId, Content = "Authentic taste! I used bacon instead of pancetta and it was amazing.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new() { RecipeId = recipes[3].Id, UserId = adminId, Content = "The key is to not overcook the eggs. Perfect recipe!", CreatedAt = DateTime.UtcNow.AddDays(-4) },
                new() { RecipeId = recipes[4].Id, UserId = demoId, Content = "Love this Buddha Bowl! Added some tofu for extra protein.", CreatedAt = DateTime.UtcNow.AddDays(-3) },
                new() { RecipeId = recipes[8].Id, UserId = demoId, Content = "The lemon garlic butter sauce is incredible. Will make again!", CreatedAt = DateTime.UtcNow.AddDays(-1) },
            };
            context.Comments.AddRange(comments);

            await context.SaveChangesAsync();
        }
    }
}
