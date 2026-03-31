# 🍳 Cooking Recipe Sharing Platform

A production-ready **ASP.NET Core MVC 8** web application for sharing, discovering, and managing cooking recipes. Inspired by **Chef.vn**, this platform provides a complete solution for food enthusiasts to create, share, and organize recipes with community engagement features.

## 📋 Features (16 Total)

### 🔍 Core Recipe Features
- **Recipe Listing & Browsing** - View all recipes with pagination and responsive grid layout
- **Recipe Search** - Full-text search across recipe titles and descriptions
- **Recipe Filtering** - Filter by category, ingredients, preparation time, and difficulty level
- **Recipe Details** - Complete recipe view with ingredients, steps, nutritional info, and images
- **Create/Edit/Delete Recipes** - Full CRUD operations for recipe management
- **Image Upload** - Upload and manage recipe images with validation

### ⭐ Community & Engagement
- **5-Star Ratings** - Rate recipes with average rating calculation
- **Comments & Reviews** - Leave comments on recipes with user attribution
- **Favorites System** - Save favorite recipes for quick access
- **Trending Section** - Display most-rated and most-favorited recipes

### 📅 Planning & Organization
- **Weekly Meal Planner** - Plan meals for the week by dragging recipes into meal slots
- **Shopping List** - Automatically generate shopping lists from planned meals
- **Shopping List Management** - Add/remove items, mark as purchased

### 👤 User Management
- **User Authentication** - Register and login with secure password hashing
- **User Profiles** - Customize profile with avatar, bio, and personal info
- **My Recipes** - Manage your own recipes
- **Admin Dashboard** - Admin panel for managing users, recipes, and categories

---

## 🛠️ Technology Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| **.NET** | 8.0 | Framework |
| **ASP.NET Core MVC** | 8.0 | Web framework |
| **Entity Framework Core** | 8.0.0 | ORM & Database |
| **SQL Server** | LocalDB | Database engine |
| **ASP.NET Identity** | 8.0.0 | Authentication & Authorization |
| **Bootstrap** | 5.3.2 | UI Framework (CDN) |
| **jQuery** | 3.6.0 | DOM manipulation (CDN) |

---

## 📁 Project Structure

```
MT3/
├── Controllers/           # 6 controllers (Home, Recipe, MealPlan, ShoppingList, Account, Admin)
├── Models/               # 11 domain models + ViewModels
├── Views/                # 51 Razor views across 6 feature areas
├── Services/             # 4 services (Recipe, MealPlan, ShoppingList, FileUpload)
├── Data/
│   ├── ApplicationDbContext.cs    # EF Core DbContext
│   └── SeedData.cs                # Initial data seeding
├── wwwroot/
│   ├── css/site.css              # Custom ChefVN branding styles
│   ├── js/                        # Client-side scripts
│   └── lib/                       # Third-party libraries
├── Program.cs            # Dependency injection & startup configuration
├── appsettings.json      # Configuration (connection string, etc.)
└── MT3.csproj           # Project file with NuGet dependencies
```

---

## 🗄️ Database Schema

**11 Tables:**
1. **AspNetUsers** - User identity data (extended by ApplicationUser)
2. **AspNetRoles** - User roles (Admin, User)
3. **AspNetUserRoles** - User-role mappings
4. **Categories** - Recipe categories (Vietnamese, Italian, etc.)
5. **Recipes** - Recipe master data
6. **Ingredients** - Ingredient catalog
7. **RecipeIngredients** - Join table (recipe ↔ ingredient with quantity)
8. **Steps** - Step-by-step cooking instructions
9. **Comments** - User comments on recipes
10. **Ratings** - 5-star user ratings (one per user per recipe)
11. **Favorites** - User's favorite recipes
12. **MealPlans** - Weekly meal planning entries
13. **ShoppingLists** - Shopping list items

---

## 🚀 Getting Started

### Prerequisites
- **.NET 8 SDK** ([Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))
- **Visual Studio 2022** or **VS Code**
- **SQL Server LocalDB** (installed with Visual Studio)

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/nh2192679-art/mt3.git
   cd MT3
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database**
   ```bash
   dotnet-ef database update
   ```
   This will create the database with seed data automatically.

4. **Run the application**
   ```bash
   dotnet run
   ```
   The app will start on **http://localhost:5105**

### 📊 Database Location

Database files are stored in:
```
C:\Users\[YourUsername]\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\
```

**Connection String** (from appsettings.json):
```json
"Server=(localdb)\\mssqllocaldb;Database=CookingAppDB;Trusted_Connection=True;MultipleActiveResultSets=true"
```

---

## 👥 Demo Accounts

After database seeding, two test accounts are available:

| Email | Password | Role |
|-------|----------|------|
| admin@example.com | Admin@123! | Admin |
| user@example.com | User@123! | User |

---

## 📖 Key Features Explained

### Recipe Management
- Create recipes with multiple ingredients and step-by-step instructions
- Upload recipe images with automatic validation
- Edit or delete your own recipes
- View recipes created by other users

### Rating & Comments
- Rate recipes on a 5-star scale (one rating per user per recipe)
- Leave detailed comments and reviews
- View community feedback on each recipe

### Meal Planning
- Drag recipes into weekly meal plan slots
- View planned meals by day of week
- Automatically generate shopping lists from planned meals

### Shopping List
- Add items manually or from planned recipes
- Toggle items as purchased
- Clear completed items

### Admin Dashboard
- Manage all users in the system
- Moderate comments
- Create/edit/delete categories
- View platform statistics

---

## 🎨 UI/UX Highlights

- **Responsive Design** - Works seamlessly on desktop, tablet, and mobile
- **Modern Styling** - ChefVN-inspired design with red/orange color scheme
- **Fast Navigation** - Quick access to recipes, favorites, and meal plans
- **Intuitive Forms** - User-friendly recipe creation and editing interfaces
- **Visual Feedback** - Hover effects, loading states, and success messages

---

## 🔒 Security Features

- **Password Hashing** - Secure password storage with ASP.NET Identity
- **HTTPS Ready** - Configured for secure connections
- **SQL Injection Prevention** - Entity Framework parameterized queries
- **Authorization** - Role-based access control (Admin vs. User)
- **File Upload Validation** - Only allow specific image formats (jpg, png, gif, webp)
- **CSRF Protection** - Anti-forgery tokens on all forms

---

## 📝 Development Notes

### Code Architecture
- **Clean Separation of Concerns** - Controllers → Services → Data Access Layer
- **Dependency Injection** - All services registered in Program.cs
- **Repository/Service Pattern** - Business logic in services, not controllers
- **ViewModels** - Separate data models for UI binding

### Database
- **Code First Approach** - Models drive database schema
- **Migrations** - Version control for database changes
- **Seed Data** - Automatic initialization with sample recipes

### Naming Conventions
- Controllers: `XxxController.cs`
- Services: `IXxxService.cs` + `XxxService.cs`
- Views: Organized by feature area (Recipe/, Account/, Admin/, etc.)
- Models: One public class per file

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is open source and available under the MIT License.

---

## 🐛 Troubleshooting

### Database Not Updating?
```bash
# Delete migrations and start fresh
dotnet-ef database drop
dotnet-ef migrations add InitialCreate
dotnet-ef database update
```

### Port Already in Use?
Edit `launchSettings.json` and change the port number.

### Can't Upload Images?
Ensure `wwwroot/uploads/recipes/` directory exists or create it manually.

---

## 📧 Support

For issues or questions:
- Open a GitHub Issue
- Check existing documentation in the `/docs` folder

---

## 📚 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/dotnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [Bootstrap 5](https://getbootstrap.com/)
- [ASP.NET Core Identity](https://docs.microsoft.com/aspnet/identity/)

---

**Made with ❤️ by your development team**

*Last Updated: March 2026*
