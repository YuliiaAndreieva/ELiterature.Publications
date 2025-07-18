using System.Collections.Generic;
using Bogus;
using Core.Dtos.Photo;
using Data.Entities;
using Data.Entities.Enums;

namespace IntegrationTests.Fakers;

public static class EntityFakers
{
    public static Faker<Author> AuthorFaker => new Faker<Author>()
        .RuleFor(a => a.FirstName, f => f.Name.FirstName())
        .RuleFor(a => a.LastName, f => f.Name.LastName())
        .RuleFor(a => a.MiddleName, f => f.Name.FirstName());

    public static Faker<LiteratureDirection> DirectionFaker => new Faker<LiteratureDirection>()
        .RuleFor(d => d.Title, f => f.Lorem.Word())
        .RuleFor(d => d.Description, f => f.Lorem.Sentence())
        .RuleFor(d => d.StartCentury, f => f.Random.Int(10, 15))
        .RuleFor(d => d.EndCentury, f => f.Random.Int(16, 20));

    public static Faker<Tag> TagFaker => new Faker<Tag>()
        .RuleFor(t => t.Title, f => f.Lorem.Word());

    public static Faker<PublicationPhotoDto> PhotoDtoFaker => new Faker<PublicationPhotoDto>()
        .RuleFor(p => p.Id, _ => 0)
        .RuleFor(p => p.PhotoUrl, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Type, _ => PhotoType.AssociatedPhoto);
    public static List<PublicationPhotoDto> GeneratePhotoDtos(int count) => PhotoDtoFaker.Generate(count);
} 