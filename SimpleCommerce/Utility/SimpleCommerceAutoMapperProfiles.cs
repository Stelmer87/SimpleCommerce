using AutoMapper;
using SimpleCommerce.Domain.Request;
using SimpleCommerce.Domain.Response;
using SimpleCommerce.Infrastructure.Entities;

namespace SimpleCommerce.Utility;

public class SimpleCommerceAutoMapperProfiles : Profile
{
    public SimpleCommerceAutoMapperProfiles()
    {
        CreateMap<AddProductRequest, ProductEntity>();
        CreateMap<ProductEntity, ProductResponse>();
        CreateMap<CartEntity, CartResponse>();
    }
}
