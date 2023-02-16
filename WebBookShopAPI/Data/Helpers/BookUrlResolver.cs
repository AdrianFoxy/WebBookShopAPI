﻿using AutoMapper;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Models;
using Microsoft.Extensions.Configuration;

namespace WebBookShopAPI.Data.Helpers
{
    public class BookUrlResolver : IValueResolver<Book, BookInCatalogDto, string>
    {
        private readonly IConfiguration _configuration;
        public BookUrlResolver(IConfiguration config) 
        {
            _configuration = config;
        }
        public string Resolve(Book source, BookInCatalogDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageURL))
            {
                return _configuration["ApiUrl"] + source.ImageURL;
            }

            return null;
        }
    }
}
