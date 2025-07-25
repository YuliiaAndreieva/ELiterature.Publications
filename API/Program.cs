using API.Configs;
using API.Graph.Filters;
using API.Graph.Queries;
using API.Graph.Types;
using API.Graph.Types.Enums;
using API.Filters;
using Core.Interfaces.Services;
using Core.Services;
using Core.Settings;
using Data.Context;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStorage(builder.Configuration);
builder.Services.AddGraphQLServer()
    .AddQueryType<RootQuery>()
    .AddType<AuthorType>()
    .AddType<PublicationType>()
    .AddType<LiteratureDirectionType>()
    .AddType<OccupationType>()
    .AddType<OrganizationType>()
    .AddType<PhotoType>()
    .AddType<PublicationPhotoType>()
    .AddType<TagType>()
    .AddType<AuthorPhotoType>()
    .AddType<PublicationTypeEnumType>()
    .AddType<PhotoTypeEnumType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .ModifyCostOptions(options =>
    {
        options.MaxFieldCost = 1_0000;
        options.MaxTypeCost = 1_0000;
        options.EnforceCostLimits = true;
        options.ApplyCostDefaults = true;
        options.DefaultResolverCost = 10.0;
    })
    .AddType<AuthorFilterType>()
    .AddType<LiteratureDirectionFilterType>();


builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.Filters.Add<GlobalExceptionFilter>();
}).AddXmlSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ILiteratureDirectionService, LiteratureDirectionService>();
builder.Services.AddScoped<IOccupationService, OccupationService>();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IAuthorPhotoRepository, AuthorPhotoRepository>();
builder.Services.AddScoped<ILiteratureDirectionRepository, LiteratureDirectionRepository>();
builder.Services.AddScoped<IOccupationRepository, OccupationRepository>();
builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IPublicationService, PublicationService>();

/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5002";
        options.Audience = "api";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            /*IssuerSigningKey = new SymmetricSecurityKey(
                Convert.FromBase64String("R1cnMNZ4VmqXy968Bv52vCIY3iS4+VicrY1YvEAHGts="))#1#
        };
    });
builder.Services.AddAuthorization();*/

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseHttpsRedirection();
/*var seeder = new DatabaseSeeder(app.Services);
await seeder.SeedAsync();*/
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ELiteratureDbContext>();
    Console.WriteLine($"[PROGRAM] DB CONNECTION: {db.Database.GetConnectionString()}");
    Console.WriteLine("[PROGRAM] MIGRATION START");
    try
    {
        // Додаємо retry logic для міграцій
        var maxRetries = 3;
        var retryDelay = TimeSpan.FromSeconds(5);
        
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                Console.WriteLine($"[PROGRAM] Migration attempt {attempt}/{maxRetries}");
                db.Database.Migrate();
                Console.WriteLine("[PROGRAM] MIGRATION END");
                break;
            }
            catch (Exception ex) when (attempt < maxRetries)
            {
                Console.WriteLine($"[PROGRAM] Migration attempt {attempt} failed: {ex.Message}");
                Console.WriteLine($"[PROGRAM] Waiting {retryDelay.TotalSeconds} seconds before retry...");
                Thread.Sleep(retryDelay);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[PROGRAM] MIGRATION ERROR: {ex}");
        throw;
    }
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:5003")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.MapGet("/playground", () => Results.Redirect("/graphql"));
app.MapControllers();
app.Run();

public partial class Program { }