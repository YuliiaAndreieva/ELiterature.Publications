using API.Graph.Types.Enums;
using Data.Entities;

namespace API.Graph.Types;

public class PublicationType : ObjectType<Publication>
{
    protected override void Configure(IObjectTypeDescriptor<Publication> descriptor)
    {
        descriptor.Name("Publication");
        descriptor.Field(p => p.Id).Type<IdType>();
        descriptor.Field(p => p.Title).Type<NonNullType<StringType>>();
        descriptor.Field(p => p.Description).Type<NonNullType<StringType>>();

        descriptor.Field(p => p.PublicationYear)
            .Type<DateType>()
            .Resolve(context =>
                context.Parent<Publication>().PublicationYear.HasValue
                    ? context.Parent<Publication>().PublicationYear.Value.ToDateTime(TimeOnly.MinValue)
                    : null);

        descriptor.Field(p => p.Type).Type<NonNullType<PublicationTypeEnumType>>();
        descriptor.Field(p => p.Text).Type<NonNullType<StringType>>();

        descriptor.Field(p => p.Authors)
            .Type<NonNullType<ListType<NonNullType<AuthorType>>>>()
            .Resolve(context =>
            {
                var writers = context.Parent<Publication>().Authors;
                return writers.Select(w => new Author
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    MiddleName = w.MiddleName
                }).ToList();
            });

        descriptor.Field(p => p.LiteratureDirection).Type<NonNullType<ListType<NonNullType<LiteratureDirectionType>>>>();
        descriptor.Field(p => p.Tags).Type<NonNullType<ListType<NonNullType<TagType>>>>();
        descriptor.Field(p => p.Photos).Type<NonNullType<ListType<NonNullType<PublicationPhotoType>>>>();
    }
}