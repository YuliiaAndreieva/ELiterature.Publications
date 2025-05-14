using Data.Entities;

namespace API.Graph.Types;

public class OccupationType : ObjectType<Occupation>
{
    protected override void Configure(IObjectTypeDescriptor<Occupation> descriptor)
    {
        descriptor.Field(o => o.Id).Type<IdType>();
        //descriptor.Field(o => o.Id).Type<IdType>();
        descriptor.Field(o => o.Title).Type<NonNullType<StringType>>();

        descriptor.Field(o => o.Authors)
            .Type<NonNullType<ListType<NonNullType<AuthorType>>>>()
            .Resolve(context =>
            {
                var writers = context.Parent<Occupation>().Authors;
                return writers.Select(w => new Author
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    MiddleName = w.MiddleName
                }).ToList();
            });
    }
}