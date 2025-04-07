using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityTypeConfigurations;

public class LiteratureStyleEntityConfiguration : IEntityTypeConfiguration<LiteratureDirection>
{
    public void Configure(
        EntityTypeBuilder<LiteratureDirection> builder)
    {
        builder
            .HasMany(ls => ls.Publications)
            .WithMany(p => p.LiteratureDirection);
    }
}