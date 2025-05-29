using Data.Entities;

namespace API.Graph.Types;

public class LiteratureDirectionType : ObjectType<LiteratureDirection>
{
    protected override void Configure(IObjectTypeDescriptor<LiteratureDirection> descriptor)
    {
        descriptor.Field(ld => ld.Id).Type<IdType>();
        descriptor.Field(ld => ld.Title).Type<NonNullType<StringType>>();
        descriptor.Field(ld => ld.Description).Type<NonNullType<StringType>>();
        descriptor.Field(ld => ld.StartCentury).Type<NonNullType<IntType>>();
        descriptor.Field(ld => ld.EndCentury).Type<IntType>();

        // Обмежуємо глибину, щоб уникнути циклічних залежностей
        descriptor.Field(ld => ld.Authors)
            .Type<NonNullType<ListType<NonNullType<AuthorType>>>>()
            .Resolve(context =>
            {
                var writers = context.Parent<LiteratureDirection>().Authors;
                return writers.Select(w => new Author
                {
                    Id = w.Id,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    MiddleName = w.MiddleName
                }).ToList();
            });

        descriptor.Field(ld => ld.Publications).Type<NonNullType<ListType<NonNullType<PublicationType>>>>();
    }
}