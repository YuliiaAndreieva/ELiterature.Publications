using Bogus;
using Core.Dtos;
using Core.Dtos.Photo;
using Core.Dtos.LiteratureDirection;
using Core.Dtos.Author;
using Data.Entities.Enums;

namespace IntegrationTests.Fakers;

public static class EntityFakers
{
    public static Faker<PublicationPhotoDto> PhotoDtoFaker => new Faker<PublicationPhotoDto>()
        .RuleFor(p => p.Id, _ => 0)
        .RuleFor(p => p.PhotoUrl, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Type, _ => PhotoType.AssociatedPhoto);

    public static Faker<AuthorPhotoDto> AuthorPhotoDtoFaker => new Faker<AuthorPhotoDto>()
        .RuleFor(p => p.Id, _ => 0)
        .RuleFor(p => p.PhotoUrl, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Type, _ => PhotoType.AssociatedPhoto)
        .RuleFor(p => p.Quote, f => f.Lorem.Sentence());

    public static Faker<CreatePublicationDto> CreatePublicationDtoFaker => new Faker<CreatePublicationDto>()
        .RuleFor(p => p.Id, _ => 0)
        .RuleFor(p => p.Title, f => f.Lorem.Sentence(3, 5))
        .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
        .RuleFor(p => p.Type, f => f.PickRandom<PublicationType>())
        .RuleFor(p => p.Text, f => f.Lorem.Paragraphs(2))
        .RuleFor(p => p.AuthorIds, _ => new List<long>())
        .RuleFor(p => p.DirectionIds, _ => new List<long>())
        .RuleFor(p => p.TagIds, _ => new List<long>())
        .RuleFor(p => p.Photos, f => GeneratePhotoDtos(f.Random.Int(1, 3)));

    public static Faker<UpdatePublicationDto> UpdatePublicationDtoFaker => new Faker<UpdatePublicationDto>()
        .RuleFor(p => p.Title, f => f.Lorem.Sentence(3, 5))
        .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
        .RuleFor(p => p.Type, f => f.PickRandom<PublicationType>())
        .RuleFor(p => p.Text, f => f.Lorem.Paragraphs(2))
        .RuleFor(p => p.AuthorIds, _ => new List<long>())
        .RuleFor(p => p.DirectionIds, _ => new List<long>())
        .RuleFor(p => p.TagIds, _ => new List<long>())
        .RuleFor(p => p.Photos, f => GeneratePhotoDtos(f.Random.Int(1, 3)));

    public static Faker<AuthorCreateDto> AuthorCreateDtoFaker => new Faker<AuthorCreateDto>()
        .RuleFor(a => a.Id, _ => 0)
        .RuleFor(a => a.FirstName, f => f.Name.FirstName())
        .RuleFor(a => a.LastName, f => f.Name.LastName())
        .RuleFor(a => a.MiddleName, f => f.Name.FirstName())
        .RuleFor(a => a.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(50)))
        .RuleFor(a => a.DateOfDeath, f => f.Random.Bool() ? DateOnly.FromDateTime(f.Date.Past(20)) : null)
        .RuleFor(a => a.Biography, f => f.Lorem.Paragraph())
        .RuleFor(a => a.DirectionIds, _ => new List<long>())
        .RuleFor(a => a.OccupationIds, _ => new List<long>())
        .RuleFor(a => a.PublicationIds, _ => new List<long>())
        .RuleFor(a => a.Photos, f => AuthorPhotoDtoFaker.Generate(f.Random.Int(0, 2)));

    public static Faker<AuthorUpdateDto> AuthorUpdateDtoFaker => new Faker<AuthorUpdateDto>()
        .RuleFor(a => a.FirstName, f => f.Name.FirstName())
        .RuleFor(a => a.LastName, f => f.Name.LastName())
        .RuleFor(a => a.MiddleName, f => f.Name.FirstName())
        .RuleFor(a => a.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(50)))
        .RuleFor(a => a.DateOfDeath, f => f.Random.Bool() ? DateOnly.FromDateTime(f.Date.Past(20)) : null)
        .RuleFor(a => a.Biography, f => f.Lorem.Paragraph())
        .RuleFor(a => a.DirectionIds, _ => new List<long>())
        .RuleFor(a => a.OccupationIds, _ => new List<long>())
        .RuleFor(a => a.PublicationIds, _ => new List<long>())
        .RuleFor(a => a.Photos, f => AuthorPhotoDtoFaker.Generate(f.Random.Int(0, 2)));

    public static Faker<LiteratureDirectionCreateDto> LiteratureDirectionCreateDtoFaker => new Faker<LiteratureDirectionCreateDto>()
        .RuleFor(d => d.Title, f => f.Lorem.Word())
        .RuleFor(d => d.Description, f => f.Lorem.Sentence())
        .RuleFor(d => d.StartCentury, f => f.Random.Int(10, 15))
        .RuleFor(d => d.EndCentury, f => f.Random.Int(16, 20));

    public static Faker<TagCreateDto> TagCreateDtoFaker => new Faker<TagCreateDto>()
        .RuleFor(t => t.Title, f => f.Lorem.Word());

    public static List<PublicationPhotoDto> GeneratePhotoDtos(int count) => PhotoDtoFaker.Generate(count);
} 