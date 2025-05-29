using Data.Entities;
using HotChocolate.Data.Filters;

namespace API.Graph.Filters;

public class LiteratureDirectionFilterType : FilterInputType<LiteratureDirection>
{
    protected override void Configure(IFilterInputTypeDescriptor<LiteratureDirection> descriptor)
    {
        descriptor.Field(f => f.Title).Type<StringOperationFilterInputType>();
        descriptor.Field(ld => ld.StartCentury).Type<IntOperationFilterInputType>();
        descriptor.Field(ld => ld.EndCentury).Type<IntOperationFilterInputType>();
    }
}