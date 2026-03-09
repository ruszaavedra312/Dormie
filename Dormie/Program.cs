using Dormie.Components;
using Dormie.Data;
using Dormie.Models;
using Dormie.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ------------------
// Add services
// ------------------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Scoped services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Database context
builder.Services.AddDbContext<DormieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ------------------
// Authentication
// ------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ------------------
// Middleware
// ------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Static assets + Blazor
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// ======================================================
// LOGIN ENDPOINT (FORM POST)
// ======================================================

app.MapPost("/api/login", async (HttpContext context, AuthService auth) =>
{
    var form = await context.Request.ReadFormAsync();

    var email = form["Email"].ToString();
    var password = form["Password"].ToString();

    var success = await auth.LoginAsync(email, password);

    if (!success)
    {
        return Results.Redirect("/login?error=invalid");
    }

    return Results.Redirect("/LoginHome");

}).AllowAnonymous();


// ======================================================
// LOGOUT ENDPOINT
// ======================================================

app.MapPost("/logout", async (AuthService auth) =>
{
    await auth.LogoutAsync();
    return Results.Redirect("/login");
});


// ======================================================
// Example Protected API
// ======================================================

app.MapGet("/api/me", (HttpContext context) =>
{
    var user = context.User.Identity?.Name;

    return Results.Ok(new { user });

}).RequireAuthorization();


app.UseAntiforgery();

app.Run();