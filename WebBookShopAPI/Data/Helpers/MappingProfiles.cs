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
                .ForMember(d => d.BookSeries, o => o.MapFrom(s => s.BookSeries.Name))
                .ForMember(d => d.Authors, o => o.MapFrom(s => s.Author.Select(n => n.FullName)))
                .ForMember(d => d.ImageURL, o=> o.MapFrom<BookUrlResolver<BookInCatalogDto>>());

            CreateMap<Book, SingleBookDto>()
                .ForMember(d => d.ImageURL, o => o.MapFrom<BookUrlResolver<SingleBookDto>>());

            CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                 .ForMember(dest => dest.BookInShopCartDto, opt => opt.MapFrom(src => src.Book));

            CreateMap<Book, BookInShopCartDto>()
                .ForMember(d => d.Authors, o => o.MapFrom(s => s.Author.Select(n => n.FullName)))
                .ForMember(d => d.ImageURL, o => o.MapFrom<BookUrlResolver<BookInShopCartDto>>());


        }
    }
}
