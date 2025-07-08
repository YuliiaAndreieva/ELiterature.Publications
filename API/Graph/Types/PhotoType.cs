using API.Graph.Types.Enums;
using Data.Entities;

namespace API.Graph.Types;

public class PhotoType : ObjectType<Photo>
{
    protected override void Configure(IObjectTypeDescriptor<Photo> descriptor)
    {
        descriptor.Field(p => p.Id).Type<IdType>();
        descriptor.Field(p => p.Type).Type<NonNullType<PhotoTypeEnumType>>();
        descriptor.Field(p => p.PhotoUrl).Type<StringType>();
    }
}