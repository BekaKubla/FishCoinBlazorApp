using Blazored.LocalStorage;
using FishCoinBlazorApp.Components;
using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Customer;
using FishCoinBlazorApp.Services;
using FishCoinBlazorApp.Services.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<FishCoinDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity-ს კონფიგურაცია
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<FishCoinDbContext>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = Microsoft.AspNetCore.Identity.IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = Microsoft.AspNetCore.Identity.IdentityConstants.ExternalScheme;
});

// ჩვენი სერვისების რეგისტრაცია
builder.Services.AddScoped<LoyaltyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CartService>();

builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();

#region Login and LogOut endpoints
app.MapGet("Account/DoLogin", async (
    string phone,
    string pass,
    bool remember,
    IAccountService accountService,
    NavigationManager nav) =>
{
    var model = new LoginModel { PhoneNumber = phone, Password = pass, RememberMe = remember };
    var result = await accountService.LoginUserAsync(model);

    if (result.Succeeded)
    {
        return Results.LocalRedirect("/");
    }

    // თუ ვერ შევიდა, დააბრუნე ლოგინზე შეცდომის მესიჯით
    return Results.LocalRedirect("/login?error=true");
});
app.MapPost("Account/Logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("/");
});
#endregion

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
