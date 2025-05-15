using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Configs;

public static class RegistrationExtensions
{
    public static void AddStorage(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString is null)
            throw new NullReferenceException(nameof(connectionString));

        serviceCollection.AddDbContext<ELiteratureDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
    }
}