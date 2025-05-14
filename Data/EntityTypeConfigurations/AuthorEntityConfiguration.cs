using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMicrosoft.EntityFrameworkCore.SqlServer.EntityTypeConfigurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(
        EntityTypeBuilder<Author> builder)
    {
        builder
            .HasMany(p => p.Publications)
            .WithMany(a => a.Authors);
        
        builder
            .HasMany(p => p.LiteratureDirection)
            .WithMany(a => a.Authors);
        
        builder
            .HasMany(p => p.Occupations)
            .WithMany(a => a.Authors);
        
        builder
            .HasMany(p => p.Organizations)
            .WithMany(a => a.Authors);
    }
}