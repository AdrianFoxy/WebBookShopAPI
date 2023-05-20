using AutoMapper;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.Identity;
using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Book, BookInCatalogDto>()
                .ForMember(d => d.Authors, o => o.MapFrom(s => s.Author.Select(n => n.FullName)))
                .ForMember(d => d.ImageURL, o=> o.MapFrom<BookUrlResolver<BookInCatalogDto>>());

            CreateMap<Book, SingleBookDto>()
                .ForMember(d => d.ImageURL, o => o.MapFrom<BookUrlResolver<SingleBookDto>>());
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Order, OrderToReturnDto>();
            CreateMap<OrderStatus, OrderStatusDto>();
            CreateMap<Delivery, DeliveryDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Book, BookInOrderDto>()
                .ForMember(d => d.ImageURL, o=>o.MapFrom<BookUrlResolver<BookInOrderDto>>());

            CreateMap<AppUser, UserListDto>();

        }
    }
}
