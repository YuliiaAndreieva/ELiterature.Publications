using API.Graph.Types.Enums;
using Data.Entities;

namespace API.Graph.Types;

public class AuthorPhotoType : ObjectType<AuthorPhoto>
{
    protected override void Configure(IObjectTypeDescriptor<AuthorPhoto> descriptor)
    {
        descriptor.Field(wp => wp.Id).Type<IdType>();
        descriptor.Field(wp => wp.Type).Type<NonNullType<PhotoTypeEnumType>>();
        descriptor.Field(wp => wp.PhotoUrl).Type<StringType>();
        descriptor.Field(wp => wp.AuthorId).Type<NonNullType<IdType>>();
        descriptor.Field(wp => wp.Author)
            .Type<NonNullType<AuthorType>>()
            .Resolve(context => new Author
            {
                Id = context.Parent<AuthorPhoto>().Author.Id,
                FirstName = context.Parent<AuthorPhoto>().Author.FirstName,
                LastName = context.Parent<AuthorPhoto>().Author.LastName,
                MiddleName = context.Parent<AuthorPhoto>().Author.MiddleName
            });
        descriptor.Field(wp => wp.Quote).Type<StringType>();
    }
}