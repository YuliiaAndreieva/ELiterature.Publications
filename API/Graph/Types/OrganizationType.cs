using Data.Entities;

namespace API.Graph.Types;

public class OrganizationType : ObjectType<Organization>
{
    protected override void Configure(IObjectTypeDescriptor<Organization> descriptor)
    {
        descriptor.Field(o => o.Id).Type<IdType>();
        descriptor.Field(o => o.Title).Type<NonNullType<StringType>>();
        descriptor.Field(o => o.Description).Type<NonNullType<StringType>>();

        descriptor.Field(o => o.StartDate)
            .Type<NonNullType<DateType>>()
            .Resolve(context => context.Parent<Organization>().StartDate.ToDateTime(TimeOnly.MinValue));

        descriptor.Field(o => o.EndDate)
            .Type<NonNullType<DateType>>()
            .Resolve(context => context.Parent<Organization>().EndDate.ToDateTime(TimeOnly.MinValue));

        descriptor.Field(o => o.Authors)
            .Type<NonNullType<ListType<AuthorType>>>()
            .Resolve(context =>
            {
                var writers = context.Parent<Organization>().Authors;
                return writers.Select(w => w != null ? new Author
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    MiddleName = w.MiddleName
                } : null).ToList();
            });
    }
}