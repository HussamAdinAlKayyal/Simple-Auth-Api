using BasicAuthApi;
using BasicAuthApi.Infrastructures;
using BasicAuthApi.Infrastructures.Errors.Handlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddDependencies();

builder.AddDbContext();
builder.AddIdentityServices();
builder.ConfigureAuthenticationAndAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>().AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

await app.MigrateDbAsync();

await app.SeedDbAsync();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
