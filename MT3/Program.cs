using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MT3.Data;
using MT3.Models;
using MT3.Services;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Services
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Đảm bảo encoding UTF-8
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.WebHost.ConfigureKestrel(options => { });
builder.Services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options => { });
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var vi = new CultureInfo("vi-VN");
    options.DefaultRequestCulture = new RequestCulture(vi);
    options.SupportedCultures = new[] { vi };
    options.SupportedUICultures = new[] { vi };
});

var app = builder.Build();

// Seed database: roles + dữ liệu mẫu + tài khoản mặc định
using (var scope = app.Services.CreateScope())
{
    await MT3.Data.SeedData.InitializeAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Force UTF-8 encoding cho tất cả response
app.Use(async (context, next) =>
{
    context.Response.Headers.ContentType = "text/html; charset=utf-8";
    await next();
});

app.UseRequestLocalization();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
