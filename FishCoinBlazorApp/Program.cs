using Blazored.LocalStorage;
using FishCoinBlazorApp.Components;
using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Entites.Customer;
using FishCoinBlazorApp.Hubs;
using FishCoinBlazorApp.Services;
using FishCoinBlazorApp.Services.Admin;
using FishCoinBlazorApp.Services.Models;
using FishCoinBlazorApp.Services.RouteServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS პოლიტიკის დამატება (რომ POS-მა შეძლოს მოთხოვნის გამოგზავნა)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PosPolicy", policy =>
    {
        policy.AllowAnyOrigin() // სატესტოდ ყველას ვუშვებთ, მერე POS-ის IP-ით შეზღუდე
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// API კონტროლერების და გვერდების მხარდაჭერა
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Swagger-ის კონფიგურაცია
builder.Services.AddEndpointsApiExplorer(); // ეს აუცილებელია მინიმალ API-ების გამოსაჩენად
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "FishCoin POS API",
        Version = "v1",
        Description = "API სალაროსთან (POS) და FishCoin-ის ქულებთან ინტეგრაციისთვის"
    });
});

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
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<FishCoinDbContext>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = Microsoft.AspNetCore.Identity.IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = Microsoft.AspNetCore.Identity.IdentityConstants.ExternalScheme;
});

// სერვისების რეგისტრაცია
builder.Services.AddScoped<LoyaltyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<WhatsAppService>();
builder.Services.AddScoped<SubCategoryService>();
builder.Services.AddScoped<RedeemStateService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddHttpClient<SmsService>();
builder.Services.AddScoped<SmsService>();

builder.Services.AddScoped<AdminProductService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Swagger-ის ჩართვა (აქ გავიტანე გარეთ, რომ დეველოპმენტზეც და სატესტოზეც ჩანდეს)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FishCoin API V1");
        // c.RoutePrefix = "swagger"; // თუ გინდა რომ პირდაპირ /swagger-ზე გაიხსნას
    });
}

app.UseHttpsRedirection();

// 2. CORS-ის გამოყენება
app.UseCors("PosPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseStaticFiles();

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

    return Results.LocalRedirect("/login?error=true");
}).ExcludeFromDescription();

app.MapPost("Account/Logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("/");
}).ExcludeFromDescription();
#endregion

// 3. კონტროლერების და ჰაბების მეპინგი
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<NotificationHub>("/orderHub");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FishCoinDbContext>(); // შენი DbContext-ის სახელი

        // ამოწმებს არის თუ არა დარჩენილი მიგრაციები და უშვებს მათ
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "მიგრაციების გაშვებისას მოხდა შეცდომა.");
    }
}

app.Run();