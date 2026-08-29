using BasicAuthApi.Infrastructures.Implementations;
using BasicAuthApi.Infrastructures.Interfaces;

namespace BasicAuthApi.Infrastructures;

public static class DependencyInjection
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IRegisterService, RegisterService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<JwtConfiguration>();
    }
}
