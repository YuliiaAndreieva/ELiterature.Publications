using Data.Context;

namespace API.Configs;

public class DatabaseSeeder
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ELiteratureDbContext>();
            var seeder = new Seeder(context);
            await seeder.SeedDataAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while seeding the database: {ex.Message}");
        }
    }
}