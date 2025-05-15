using API.Configs;
using API.Graph.Queries;
using API.Graph.Types;
using API.Graph.Types.Enums;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStorage(builder.Configuration);
builder.Services.AddGraphQLServer()
    .AddQueryType<AuthorQuery>()
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
    .AddProjections();

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
}).AddXmlSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5002";
        options.Audience = "api";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Convert.FromBase64String("R1cnMNZ4VmqXy968Bv52vCIY3iS4+VicrY1YvEAHGts="))
        };
    });
builder.Services.AddAuthorization();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
/*var seeder = new DatabaseSeeder(app.Services);
await seeder.SeedAsync();*/

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