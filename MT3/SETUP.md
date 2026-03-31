# 🔧 Installation & Setup Guide

## Prerequisites

Ensure you have the following installed:

- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **Visual Studio 2022 Community/Professional** or **VS Code**
- **SQL Server LocalDB** (comes with Visual Studio)
- **Git** - [Download](https://git-scm.com/)

---

## Step 1: Clone Repository

```bash
git clone https://github.com/nh2192679-art/mt3.git
cd MT3
```

---

## Step 2: Restore Dependencies

```bash
dotnet restore
```

This downloads all NuGet packages specified in `MT3.csproj`.

---

## Step 3: Verify Database Connection

Open `appsettings.json` and confirm the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CookingAppDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

**Components:**
- `Server` - LocalDB instance (built-in with Visual Studio)
- `Database` - Database name: `CookingAppDB`
- `Trusted_Connection` - Uses Windows authentication

---

## Step 4: Create Database & Run Migrations

```bash
# Install EF Core CLI globally (if not already installed)
dotnet tool install --global dotnet-ef --version 8.0.0

# Navigate to project directory
cd MT3

# Apply migrations and create database
dotnet-ef database update
```

**Output should show:**
```
Build started...
Build completed successfully.
Applying migration '20260331204958_InitialCreate'.
Done.
```

This will:
- ✅ Create `CookingAppDB` database
- ✅ Create 11 tables with relationships
- ✅ Load seed data (5 categories, 10 recipes, 2 users, etc.)

---

## Step 5: Run the Application

### Option A: Command Line
```bash
cd MT3
dotnet run
```

### Option B: Visual Studio
1. Open `MT3.sln` in Visual Studio
2. Set `MT3` as the startup project
3. Press `F5` to run

**Expected Output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5105
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

---

## Step 6: Access the Application

Open your browser and navigate to:

```
http://localhost:5105
```

You should see the home page with featured recipes!

---

## 🧪 Test Demo Accounts

**After database seeding, use these accounts:**

### Admin Account
- **Email:** `admin@example.com`
- **Password:** `Admin@123!`
- **Access:** Full admin dashboard

### User Account
- **Email:** `user@example.com`
- **Password:** `User@123!`
- **Access:** Create recipes, rate, comment, plan meals

---

## 📂 Database Files Location

Database files are automatically created at:

```
C:\Users\[YourUsername]\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\
```

### Access with SQL Server Management Studio (SSMS)

1. Open **SQL Server Management Studio**
2. Connect to server: `(localdb)\mssqllocaldb`
3. Navigate to **Databases** → **CookingAppDB**
4. View tables, data, and indexes

---

## 🐛 Common Issues & Solutions

### Issue: Database Update Fails

**Error:** "Unable to locate the .NET Core SDK"

**Solution:**
```bash
dotnet --version  # Check if .NET 8 is installed
dotnet sdk check  # List available SDKs
```

---

### Issue: Port 5105 Already in Use

**Error:** "Address already in use"

**Solution:**
1. Open `Properties/launchSettings.json`
2. Change port number (e.g., `5106`)
3. Run again

---

### Issue: EF Core CLI Not Found

**Error:** "dotnet-ef: The term 'dotnet-ef' is not recognized"

**Solution:**
```bash
# Install globally
dotnet tool install --global dotnet-ef --version 8.0.0

# Or reinstall
dotnet tool update --global dotnet-ef --version 8.0.0
```

---

### Issue: Database File Locked

**Error:** "File is locked or in use"

**Solution:**
1. Close Visual Studio
2. Close any open SQL Server Management Studio instances
3. Run again

---

### Issue: Seed Data Not Loading

**Error:** Database created but no test data

**Solution:**
```bash
# Delete existing database
dotnet-ef database drop --force

# Recreate with fresh seed data
dotnet-ef database update
```

---

## ✅ Verification Checklist

After setup, verify everything is working:

- [ ] Application runs on `http://localhost:5105`
- [ ] Home page loads with recipe cards
- [ ] Can log in with `admin@example.com` / `Admin@123!`
- [ ] Admin dashboard is accessible
- [ ] Can view recipe details
- [ ] Can create a new recipe (when logged in)
- [ ] Search functionality works
- [ ] Category filter works
- [ ] Meal planner page loads
- [ ] Shopping list page works

---

## 🚀 Development Workflow

### Run with Hot Reload
```bash
dotnet watch run
```

Changes to code will automatically reload the application.

### Run Tests
```bash
dotnet test
```

### Build for Release
```bash
dotnet build -c Release
```

### Publish for Deployment
```bash
dotnet publish -c Release -o ./publish
```

---

## 📋 Project Structure

```
MT3/
├── Controllers/          # HTTP request handlers (6 controllers)
├── Models/              # Domain models + ViewModels (15 classes)
├── Views/               # Razor templates (51 views)
├── Services/            # Business logic (4 services)
├── Data/                # Database context & seeding
├── wwwroot/             # Static files (CSS, JS, images)
├── Properties/          # Launch settings, build config
├── Program.cs           # Startup configuration
├── appsettings.json     # Configuration
└── MT3.csproj           # Project file
```

---

## 📖 Next Steps

1. **Explore the codebase** - Read through models, services, and controllers
2. **Test features** - Create recipes, rate them, use meal planner
3. **Customize styling** - Edit `wwwroot/css/site.css` for your brand
4. **Add new features** - Follow the existing patterns to extend functionality
5. **Deploy** - See deployment guide for production setup

---

## 🔗 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/dotnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [Bootstrap 5 Docs](https://getbootstrap.com/docs/)
- [C# Language Reference](https://docs.microsoft.com/dotnet/csharp/)

---

## ❓ Questions?

- Check the main [README.md](./README.md) for feature overview
- Review code comments in key files
- Check GitHub Issues for common questions

---

**Happy Coding! 🎉**
