using System.Reflection;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ELiteratureDbContext : DbContext 
{
    public ELiteratureDbContext()
    {
    }

    public ELiteratureDbContext(DbContextOptions<ELiteratureDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Writer?> Writers { get; set; }
    
    public DbSet<Publication> Publications { get; set; }
    
    public DbSet<LiteratureDirection> LiteratureDirections { get; set; }
    
    public DbSet<Occupation> Occupations { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
    
    public DbSet<Photo> Photos { get; set; }
    
    public DbSet<Organization> Organizations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}