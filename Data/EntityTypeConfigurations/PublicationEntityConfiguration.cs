using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityTypeConfigurations;

public class PublicationEntityConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(
        EntityTypeBuilder<Publication> builder)
    {
        builder
            .Property(p => p.Type)
            .HasConversion<string>();
    }
}