﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ELiteratureDbContext))]
    [Migration("20250514171335_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthorLiteratureDirection", b =>
                {
                    b.Property<long>("AuthorsId")
                        .HasColumnType("bigint");

                    b.Property<long>("LiteratureDirectionId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthorsId", "LiteratureDirectionId");

                    b.HasIndex("LiteratureDirectionId");

                    b.ToTable("AuthorLiteratureDirection");
                });

            modelBuilder.Entity("AuthorOccupation", b =>
                {
                    b.Property<long>("AuthorsId")
                        .HasColumnType("bigint");

                    b.Property<long>("OccupationsId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthorsId", "OccupationsId");

                    b.HasIndex("OccupationsId");

                    b.ToTable("AuthorOccupation");
                });

            modelBuilder.Entity("AuthorOrganization", b =>
                {
                    b.Property<long>("AuthorsId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrganizationsId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthorsId", "OrganizationsId");

                    b.HasIndex("OrganizationsId");

                    b.ToTable("AuthorOrganization");
                });

            modelBuilder.Entity("AuthorPublication", b =>
                {
                    b.Property<long>("AuthorsId")
                        .HasColumnType("bigint");

                    b.Property<long>("PublicationsId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthorsId", "PublicationsId");

                    b.HasIndex("PublicationsId");

                    b.ToTable("AuthorPublication");
                });

            modelBuilder.Entity("Data.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateOfDeath")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Data.Entities.LiteratureDirection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EndCentury")
                        .HasColumnType("bigint");

                    b.Property<long>("StartCentury")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LiteratureDirections");
                });

            modelBuilder.Entity("Data.Entities.Occupation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Occupations");
                });

            modelBuilder.Entity("Data.Entities.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Data.Entities.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Photos");

                    b.HasDiscriminator().HasValue("Photo");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Data.Entities.Publication", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("PublicationYear")
                        .HasColumnType("date");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("Data.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LiteratureDirectionPublication", b =>
                {
                    b.Property<long>("LiteratureDirectionId")
                        .HasColumnType("bigint");

                    b.Property<long>("PublicationsId")
                        .HasColumnType("bigint");

                    b.HasKey("LiteratureDirectionId", "PublicationsId");

                    b.HasIndex("PublicationsId");

                    b.ToTable("LiteratureDirectionPublication");
                });

            modelBuilder.Entity("PublicationTag", b =>
                {
                    b.Property<long>("PublicationsId")
                        .HasColumnType("bigint");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint");

                    b.HasKey("PublicationsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("PublicationTag");
                });

            modelBuilder.Entity("Data.Entities.AuthorPhoto", b =>
                {
                    b.HasBaseType("Data.Entities.Photo");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Quote")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AuthorId");

                    b.HasDiscriminator().HasValue("WriterPhoto");
                });

            modelBuilder.Entity("Data.Entities.PublicationPhoto", b =>
                {
                    b.HasBaseType("Data.Entities.Photo");

                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint");

                    b.HasIndex("PublicationId");

                    b.HasDiscriminator().HasValue("PublicationPhoto");
                });

            modelBuilder.Entity("AuthorLiteratureDirection", b =>
                {
                    b.HasOne("Data.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.LiteratureDirection", null)
                        .WithMany()
                        .HasForeignKey("LiteratureDirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorOccupation", b =>
                {
                    b.HasOne("Data.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Occupation", null)
                        .WithMany()
                        .HasForeignKey("OccupationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorOrganization", b =>
                {
                    b.HasOne("Data.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorPublication", b =>
                {
                    b.HasOne("Data.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LiteratureDirectionPublication", b =>
                {
                    b.HasOne("Data.Entities.LiteratureDirection", null)
                        .WithMany()
                        .HasForeignKey("LiteratureDirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PublicationTag", b =>
                {
                    b.HasOne("Data.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.AuthorPhoto", b =>
                {
                    b.HasOne("Data.Entities.Author", "Author")
                        .WithMany("Photos")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Data.Entities.PublicationPhoto", b =>
                {
                    b.HasOne("Data.Entities.Publication", "Publication")
                        .WithMany("Photos")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("Data.Entities.Author", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Data.Entities.Publication", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
