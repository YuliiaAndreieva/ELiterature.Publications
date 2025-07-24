using Data.Context;
using Microsoft.EntityFrameworkCore;
using Data.Repositories;
using Core.Services;
using Data.Repositories.Interfaces;

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
            options.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
            })
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        serviceCollection.AddScoped<IRawSqlRepository, RawSqlRepository>();
        serviceCollection.AddScoped<RawSqlService>();
    }
}