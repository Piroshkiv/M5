using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderInfo, OrderInfoDto>()
            .ForMember(
                o => o.Products, 
                    i => i.MapFrom(
                        o => o.Products.Select( p => new OrderProductDto { Id = p.Id, Order = p.OrderId, Product = p.ProductId, Quantity = p.Quantity} )));

        CreateMap<OrderInfoDto, OrderInfo>()
            .ForMember(
                o => o.Products,
                    i => i.MapFrom(
                        o => o.Products.Select(p => new OrderProduct { Id = p.Id, OrderId = p.Order, ProductId = p.Product, Quantity = p.Quantity })));

        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember( o => o.Product, i=> i.MapFrom( o => o.ProductId ) )
            .ForMember(o => o.Order, i => i.MapFrom(o => o.OrderId));

        CreateMap<OrderProductDto, OrderProduct>()
            .ForMember(o => o.ProductId, i => i.MapFrom(o => o.Product))
            .ForMember(o => o.OrderId, i => i.MapFrom(o => o.Order));
    }
}