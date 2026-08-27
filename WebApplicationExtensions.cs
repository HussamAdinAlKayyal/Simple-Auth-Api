using BasicAuthApi.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthApi;

public static class WebApplicationExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
}
