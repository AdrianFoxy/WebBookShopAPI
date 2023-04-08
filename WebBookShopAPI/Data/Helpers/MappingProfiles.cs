using AutoMapper;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Models;

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

        }
    }
}
