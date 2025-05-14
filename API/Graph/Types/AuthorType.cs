using Data.Entities;

namespace API.Graph.Types;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field(w => w.Id).Type<IdType>();

        descriptor.Field(w => w.FirstName).Type<NonNullType<StringType>>();
        descriptor.Field(w => w.LastName).Type<NonNullType<StringType>>();
        descriptor.Field(w => w.MiddleName).Type<NonNullType<StringType>>();
        descriptor.Field(w => w.DateOfBirth)
            .Type<NonNullType<DateType>>()
            .Resolve(context =>
            {
                var writer = context.Parent<Author>();
                return writer.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            });

        descriptor.Field(w => w.DateOfDeath)
            .Type<DateType>()
            .Resolve(context =>
            {
                var writer = context.Parent<Author>();
                return writer.DateOfDeath.HasValue
                    ? writer.DateOfDeath.Value.ToDateTime(TimeOnly.MinValue)
                    : null;
            });

        descriptor.Field(w => w.Biography).Type<StringType>();
        
        descriptor.Field(w => w.Publications).Type<NonNullType<ListType<NonNullType<PublicationType>>>>();
        descriptor.Field(w => w.LiteratureDirection).Type<NonNullType<ListType<NonNullType<LiteratureDirectionType>>>>();
        descriptor.Field(w => w.Occupations).Type<NonNullType<ListType<NonNullType<OccupationType>>>>();
        descriptor.Field(w => w.Organizations).Type<NonNullType<ListType<NonNullType<OrganizationType>>>>();
        descriptor.Field(w => w.Photos).Type<NonNullType<ListType<NonNullType<AuthorPhotoType>>>>();
    }
}