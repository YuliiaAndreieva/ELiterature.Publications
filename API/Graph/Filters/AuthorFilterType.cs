using Data.Entities;
using HotChocolate.Data.Filters;

namespace API.Graph.Filters;

public class AuthorFilterType : FilterInputType<Author>
{
    protected override void Configure(IFilterInputTypeDescriptor<Author> descriptor)
    {
        descriptor.Field(a => a.LiteratureDirection)
            .Type<ListType<LiteratureDirectionFilterType>>();
    }
}