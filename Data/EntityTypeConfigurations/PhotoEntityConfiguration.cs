using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityTypeConfigurations;

public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(
        EntityTypeBuilder<Photo> builder)
    {
        builder
            .HasDiscriminator<string>("Discriminator")
            .HasValue<AuthorPhoto>("WriterPhoto")
            .HasValue<PublicationPhoto>("PublicationPhoto");
    }
}