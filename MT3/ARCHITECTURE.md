# 📁 Project Structure & Architecture Guide

## Directory Overview

```
MT3/
├── 📂 Controllers/              # HTTP request handlers
├── 📂 Data/                     # Database context & migrations
├── 📂 Models/                   # Domain entities + ViewModels
├── 📂 Services/                 # Business logic layer
├── 📂 Views/                    # Razor view templates
├── 📂 wwwroot/                  # Static assets (CSS, JS, images)
├── 📂 Properties/               # Visual Studio project settings
├── 📄 Program.cs                # App startup & DI configuration
├── 📄 appsettings.json          # Configuration & connection strings
├── 📄 MT3.csproj                # Project file with NuGet references
├── 📄 README.md                 # Feature overview
└── 📄 SETUP.md                  # Installation guide
```

---

## 🎮 Controllers (6 Total)

**Purpose:** Handle HTTP requests and return responses

### HomeController.cs
```
GET /                           → Home page with featured recipes
GET /Search?query=...&category= → Search & filter recipes
```

### RecipeController.cs (Main Feature)
```
GET  /Recipe                    → List all recipes (paginated)
GET  /Recipe/{id}               → View recipe details
GET  /Recipe/Create             → Show create form
POST /Recipe/Create             → Save new recipe
GET  /Recipe/{id}/Edit          → Show edit form
POST /Recipe/{id}/Edit          → Update recipe
POST /Recipe/{id}/Delete        → Delete recipe
POST /Recipe/{id}/Rate          → Submit star rating
POST /Recipe/{id}/ToggleFavorite → Add/remove favorite
POST /Recipe/{id}/Comments      → Add comment
GET  /Recipe/MyRecipes          → View your recipes
GET  /Recipe/Favorites          → View favorite recipes
```

### MealPlanController.cs
```
GET  /MealPlan                  → View weekly meal planner
POST /MealPlan/AddMeal          → Add recipe to meal slot
POST /MealPlan/RemoveMeal       → Remove meal from slot
```

### ShoppingListController.cs
```
GET  /ShoppingList              → View shopping list
POST /ShoppingList/AddFromRecipe → Generate from recipe ingredients
POST /ShoppingList/Toggle       → Mark item as purchased/unpurchased
POST /ShoppingList/Remove       → Delete item
POST /ShoppingList/Clear        → Clear completed items
```

### AccountController.cs
```
GET  /Account/Login             → Login page
POST /Account/Login             → Process login
GET  /Account/Register          → Registration page
POST /Account/Register          → Create account
POST /Account/Logout            → Sign out
GET  /Account/Profile           → View profile
POST /Account/UpdateProfile     → Update profile info
GET  /Account/AccessDenied      → Permission denied page
```

### AdminController.cs
```
GET  /Admin                     → Dashboard with statistics
GET  /Admin/Users               → Manage users
POST /Admin/Users/{id}/Role     → Change user role
POST /Admin/Users/{id}/Disable  → Disable user account
GET  /Admin/Recipes             → Review user recipes
POST /Admin/Recipes/{id}/Delete → Remove recipe
GET  /Admin/Comments            → Moderate comments
POST /Admin/Comments/{id}/Delete → Delete comment
GET  /Admin/Categories          → Manage categories
POST /Admin/Categories          → Create category
GET  /Admin/Stats               → View platform statistics
```

---

## 📊 Data Models (11 + ViewModels)

### Domain Models (in Models folder)

**Category.cs**
```csharp
- CategoryId (PK)
- Name (string)
- Description (string)
- CreatedAt (DateTime)
- Recipes (ICollection<Recipe>)
```

**Recipe.cs** ⭐ Core Entity
```csharp
- RecipeId (PK)
- Title (string)
- Description (string)
- Difficulty (Easy/Medium/Hard)
- PrepTime (int - minutes)
- CookTime (int - minutes)
- Servings (int)
- ImageUrl (string)
- CreatedBy (FK → ApplicationUser)
- CategoryId (FK → Category)
- CreatedAt (DateTime)
- ComputedProperties:
  - AverageRating (decimal)
  - RatingCount (int)
- Relationships:
  - Category, ApplicationUser, Ingredients, Steps, Comments, Ratings, Favorites
```

**Ingredient.cs**
```csharp
- IngredientId (PK)
- Name (string)
- Unit (string - "cup", "g", "tbsp")
- RecipeIngredients (ICollection<RecipeIngredient>)
```

**RecipeIngredient.cs** (Join Table)
```csharp
- RecipeIngredientId (PK)
- RecipeId (FK)
- IngredientId (FK)
- Quantity (decimal)
- Notes (string)
- Recipe, Ingredient (navigation properties)
```

**Step.cs**
```csharp
- StepId (PK)
- RecipeId (FK)
- StepNumber (int - order)
- Instructions (string)
- Tip (string)
- Recipe (navigation)
```

**Comment.cs**
```csharp
- CommentId (PK)
- RecipeId (FK)
- UserId (FK)
- Content (string)
- CreatedAt (DateTime)
- Recipe, User (navigation)
```

**Rating.cs**
```csharp
- RatingId (PK)
- RecipeId (FK)
- UserId (FK)
- Score (int - 1 to 5)
- CreatedAt (DateTime)
- Unique constraint: (RecipeId, UserId) - one rating per user per recipe
```

**Favorite.cs**
```csharp
- FavoriteId (PK)
- RecipeId (FK)
- UserId (FK)
- CreatedAt (DateTime)
- Unique constraint: (RecipeId, UserId)
```

**MealPlan.cs**
```csharp
- MealPlanId (PK)
- UserId (FK)
- RecipeId (FK)
- MealDate (DateTime)
- MealType (Breakfast/Lunch/Dinner)
- User, Recipe (navigation)
```

**ShoppingList.cs**
```csharp
- ShoppingListId (PK)
- UserId (FK)
- ItemName (string)
- Quantity (string)
- Unit (string)
- IsChecked (bool)
- CreatedAt (DateTime)
```

**ApplicationUser.cs** (Extends IdentityUser)
```csharp
- (Inherited from IdentityUser: Id, Email, PasswordHash, etc.)
- FullName (string)
- AvatarUrl (string)
- Bio (string)
- JoinedAt (DateTime)
- Recipes (ICollection<Recipe>)
- Comments, Ratings, Favorites, MealPlans, ShoppingLists (collections)
```

### ViewModels (for UI binding)

**HomeViewModel**
```
- FeaturedRecipes (List<RecipeCardViewModel>)
- TrendingRecipes (List<RecipeCardViewModel>)
- RecentComments (List<CommentViewModel>)
```

**RecipeListViewModel**
```
- Recipes (List<RecipeCardViewModel>)
- Categories (List<CategoryDTO>)
- SearchQuery (string)
- SelectedCategoryId (int?)
- TotalRecipes (int)
- PageNumber (int)
- PageSize (int)
- TotalPages (computed)
```

**RecipeDetailViewModel**
```
- Recipe (Recipe)
- Ingredients (List<RecipeIngredient>)
- Steps (List<Step>)
- Comments (List<Comment>)
- Ratings (List<Rating>)
- UserRating (Rating) - current user's rating
- IsFavorite (bool) - is bookmarked by user
- AverageRating (decimal)
```

**RecipeCreateEditViewModel**
```
- RecipeId (int?)
- Title (string)
- Description (string)
- Difficulty (string)
- PrepTime (int)
- CookTime (int)
- Servings (int)
- CategoryId (int)
- Ingredients (List<IngredientDTO>)
- Steps (List<StepDTO>)
- ImageFile (IFormFile)
- Categories (List<SelectListItem>)
```

**MealPlanViewModel**
```
- PlanByDay (Dictionary<DateTime, List<MealSlot>>)
- AvailableRecipes (List<Recipe>)
```

**ShoppingListViewModel**
```
- Items (List<ShoppingListItem>)
- CompletedCount (int)
- PendingCount (int)
```

---

## 💼 Services (4 Total)

**Purpose:** Encapsulate business logic, used by controllers

### IRecipeService & RecipeService

**Methods:**
```csharp
GetRecipesAsync(search?, categoryId?, skip=0, take=10)
  → Returns paginated recipe list with filters

GetRecipeDetailAsync(recipeId)
  → Returns complete recipe with ingredients, steps, comments, ratings

GetUserRecipesAsync(userId)
  → Returns recipes created by user

SearchRecipesAsync(query)
  → Full-text search across titles and descriptions

FilterByCategoryAsync(categoryId)
  → Get recipes in category

RateRecipeAsync(recipeId, userId, score)
  → Create/update rating (one per user)

GetAverageRatingAsync(recipeId)
  → Calculate recipe rating

ToggleFavoriteAsync(recipeId, userId)
  → Add/remove favorite

AddCommentAsync(recipeId, userId, content)
  → Post comment on recipe

GetCommentsAsync(recipeId)
  → Fetch comments for recipe

CreateRecipeAsync(recipe, userId)
  → Create new recipe

UpdateRecipeAsync(recipe, userId)
  → Update recipe

DeleteRecipeAsync(recipeId, userId)
  → Delete recipe (ownership check)
```

### IMealPlanService & MealPlanService

**Methods:**
```csharp
GetWeeklyPlanAsync(userId)
  → Returns meals organized by day

AddMealAsync(userId, recipeId, mealDate, mealType)
  → Add recipe to meal slot

RemoveMealAsync(mealPlanId, userId)
  → Remove meal from plan

GenerateShoppingListAsync(userId)
  → Extract ingredients from planned meals
```

### IShoppingListService & ShoppingListService

**Methods:**
```csharp
GetListAsync(userId)
  → Get user's shopping items

AddItemAsync(userId, itemName, quantity, unit)
  → Add item manually

AddFromRecipeAsync(userId, recipeId)
  → Add all ingredients from recipe

ToggleItemAsync(itemId, userId)
  → Mark as done/pending

RemoveItemAsync(itemId, userId)
  → Delete item

ClearCompletedAsync(userId)
  → Remove all checked items
```

### IFileUploadService & FileUploadService

**Methods:**
```csharp
UploadImageAsync(file, recipeId)
  → Upload image with validation
  → Validates: .jpg, .png, .gif, .webp only
  → Max size: 5MB
  → Returns: relative URL path

DeleteImageAsync(fileUrl)
  → Remove uploaded file
```

---

## 🎨 Views (51 Total)

### Shared/ (Common Layout)
```
_Layout.cshtml          - Master layout with navbar, footer
_LoginPartial.cshtml    - Auth status display
```

### Home/
```
Index.cshtml            - Hero section, featured recipes, trending
```

### Recipe/ (5 views)
```
Index.cshtml            - Recipe listing with pagination & filters
Details.cshtml          - Full recipe view with ingredients & steps
Create.cshtml           - Form to create new recipe
Edit.cshtml             - Form to edit recipe
MyRecipes.cshtml        - User's own recipes
Favorites.cshtml        - Bookmarked recipes
```

### Account/ (4 views)
```
Login.cshtml            - Email/password login
Register.cshtml         - New account signup
Profile.cshtml          - View user profile
AccessDenied.cshtml     - Permission error
```

### Admin/ (5+ views)
```
Index.cshtml            - Dashboard with stats cards
Users/Index.cshtml      - Manage users table
Recipes/Index.cshtml    - Review recipes
Comments/Index.cshtml   - Moderate comments
Categories/Index.cshtml - Manage categories
```

### MealPlan/
```
Index.cshtml            - Weekly calendar grid
```

### ShoppingList/
```
Index.cshtml            - Checklist view
```

---

## 🗄️ Data Access Layer

### ApplicationDbContext.cs

**DbSets (11 tables):**
```csharp
DbSet<Category>
DbSet<Recipe>
DbSet<Ingredient>
DbSet<RecipeIngredient>
DbSet<Step>
DbSet<Comment>
DbSet<Rating>
DbSet<Favorite>
DbSet<MealPlan>
DbSet<ShoppingList>
DbSet<ApplicationUser> (from Identity)
```

**Key Configurations in OnModelCreating:**
- Relationship mappings (HasOne, WithMany)
- Cascade/Restrict delete behaviors
- Unique constraints on ratings, favorites
- Foreign key configurations
- Shadow properties for timestamps

### SeedData.cs

**Seed Data:**
- 5 Categories (Vietnamese, Italian, Asian, Desserts, Appetizers)
- 10 Recipes with full metadata
- 20 Ingredients
- 2 Users (admin + regular user)
- 15+ Ratings
- 10+ Comments
- 5+ Favorites

---

## 🎯 Architecture Pattern

### Clean Layered Architecture

```
┌─────────────────────┐
│  Views (Razor)      │
│  (Presentation)     │
└──────────┬──────────┘
           │
┌──────────▼──────────┐
│  Controllers        │
│  (Request Handler)  │
└──────────┬──────────┘
           │
┌──────────▼──────────┐
│  Services (IService)│
│  (Business Logic)   │
└──────────┬──────────┘
           │
┌──────────▼──────────┐
│  DbContext          │
│  (Data Access)      │
└──────────┬──────────┘
           │
┌──────────▼──────────┐
│  Database           │
│  (SQL Server)       │
└─────────────────────┘
```

### Dependency Injection

Configured in `Program.cs`:
```csharp
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
```

---

## 🔄 Request Flow Example: View Recipe Details

```
1. User visits /Recipe/123
   ↓
2. RecipeController.Details(123) called
   ↓
3. Service method: RecipeService.GetRecipeDetailAsync(123)
   ↓
4. DbContext queries database:
   - Recipe table (by RecipeId)
   - Include ingredients via RecipeIngredient
   - Include steps
   - Include comments with user info
   - Include ratings
   ↓
5. Data mapped to RecipeDetailViewModel
   ↓
6. View rendered: Recipe/Details.cshtml
   ↓
7. HTML returned to browser
```

---

## 📝 Key File Locations

| File | Purpose |
|------|---------|
| `Program.cs` | DI container & startup config |
| `appsettings.json` | Connection string & settings |
| `ApplicationDbContext.cs` | Database schema & relationships |
| `SeedData.cs` | Initial data population |
| `RecipeService.cs` | Recipe business logic (most important) |
| `_Layout.cshtml` | Master page template |
| `Recipe/Details.cshtml` | Recipe detail page (core feature) |
| `site.css` | Styling & theme |
| `Migrations/` | Database version control |

---

## 🚀 Adding New Feature: Step-by-Step

1. **Create Model** → `Models/NewFeature.cs`
2. **Add DbSet** → `ApplicationDbContext.cs`
3. **Configure Relationships** → `OnModelCreating()`
4. **Create Service** → `Services/INewFeatureService.cs` + implementation
5. **Register Service** → `Program.cs`
6. **Create Controller** → `Controllers/NewFeatureController.cs`
7. **Create Views** → `Views/NewFeature/*.cshtml`
8. **Create Migration** → `dotnet-ef migrations add AddNewFeature`
9. **Update Database** → `dotnet-ef database update`
10. **Test** → Verify functionality works

---

## 📚 Related Documentation

- [Entity Framework Code First](https://docs.microsoft.com/ef/core/get-started/overview/first-app)
- [ASP.NET MVC Pattern](https://docs.microsoft.com/aspnet/mvc/overview/older-versions-1/)
- [Razor View Engine](https://docs.microsoft.com/aspnet/core/mvc/views/razor)

