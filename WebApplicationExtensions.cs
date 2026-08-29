using BasicAuthApi.Infrastructures.Data;
using BasicAuthApi.Infrastructures.Errors;
using BasicAuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthApi;

public static class WebApplicationExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }

    public static async Task SeedDbAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        User? user = await userManager.FindByNameAsync("Admin");
        IdentityResult userResult, adminRoleResult, userRoleResult, identityResult;

        if (user is null)
        {
            user = new("Admin", "First name of the admin", "His or her last name", "admin@admin.ceo");
            userResult = await userManager.CreateAsync(user, "Admin-123");
            userResult.ThrowIfNotSucceeded();
        }

        IdentityRole? adminRole = await roleManager.FindByNameAsync("Admin");

        if (adminRole is null)
        {
            adminRoleResult = await roleManager.CreateAsync(new("Admin"));
            adminRoleResult.ThrowIfNotSucceeded();
        }

        IdentityRole? userRole = await roleManager.FindByNameAsync("User");

        if (userRole is null)
        {
            userRoleResult = await roleManager.CreateAsync(new("User"));
            userRoleResult.ThrowIfNotSucceeded();
        }

        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            identityResult = await userManager.AddToRoleAsync(user, "Admin");
            identityResult.ThrowIfNotSucceeded();
        }

        if (!await userManager.IsInRoleAsync(user, "User"))
        {
            identityResult = await userManager.AddToRoleAsync(user, "User");
            identityResult.ThrowIfNotSucceeded();
        }
    }
}
