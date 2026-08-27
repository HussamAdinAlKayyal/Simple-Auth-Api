using BasicAuthApi.Infrastructures.Data;
using BasicAuthApi.Infrastructures.Implementations;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthApi.Infrastructures;

public static class DependencyInjection
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IRegisterService, RegisterService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<IUserService, UserService>();
    }

    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
    }

    public static void AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
