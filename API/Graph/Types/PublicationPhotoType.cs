using API.Graph.Types.Enums;
using Data.Entities;

namespace API.Graph.Types;

public class PublicationPhotoType : ObjectType<PublicationPhoto>
{
    protected override void Configure(IObjectTypeDescriptor<PublicationPhoto> descriptor)
    {
        descriptor.Field(pp => pp.Id).Type<IdType>();
        descriptor.Field(pp => pp.Type).Type<NonNullType<PhotoTypeEnumType>>();
        descriptor.Field(pp => pp.PhotoUrl).Type<StringType>();
        descriptor.Field(pp => pp.PublicationId).Type<NonNullType<IdType>>();
        descriptor.Field(pp => pp.Publication).Type<NonNullType<PublicationType>>();
    }
}