using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMicrosoft.EntityFrameworkCore.SqlServer.EntityTypeConfigurations;

public class WriterEntityConfiguration : IEntityTypeConfiguration<Writer>
{
    public void Configure(
        EntityTypeBuilder<Writer> builder)
    {
        builder
            .HasMany(p => p.Publications)
            .WithMany(a => a.Writers);
        
        builder
            .HasMany(p => p.LiteratureDirection)
            .WithMany(a => a.Writers);
        
        builder
            .HasMany(p => p.Occupations)
            .WithMany(a => a.Writers);
        
        builder
            .HasMany(p => p.Organizations)
            .WithMany(a => a.Writers);
    }
}