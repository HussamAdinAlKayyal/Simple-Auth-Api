namespace BasicAuthApi.Infrastructures.Implementations;

public class JwtConfiguration
{
    public string Issuer { get; }
    public string Audience { get; }
    public string Key { get; }
    public double ExpiresInMin { get; }

    public JwtConfiguration(IConfiguration configuration)
    {
        IConfigurationSection section = configuration.GetRequiredSection("Jwt");
        Issuer = section["Issuer"]!;
        Audience = section["Audience"]!;
        Key = section["Key"]!;
        ExpiresInMin = section.GetValue<double>("ExpiresInMin");
    }
}
