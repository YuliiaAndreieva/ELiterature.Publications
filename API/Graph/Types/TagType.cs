using Data.Entities;
using Tag = Data.Entities.Tag;

namespace API.Graph.Types;

public class TagType : ObjectType<Tag>
{
    protected override void Configure(IObjectTypeDescriptor<Tag> descriptor)
    {
        descriptor.Field(t => t.Id).Type<IdType>();
        descriptor.Field(t => t.Title).Type<NonNullType<StringType>>();

        descriptor.Field(t => t.Publications)
            .Type<NonNullType<ListType<NonNullType<PublicationType>>>>()
            .Resolve(context =>
            {
                var publications = context.Parent<Tag>().Publications ?? new List<Publication>();
                return publications.Select(p => new Publication
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Text = p.Text,
                }).ToList();
            });
    }
}