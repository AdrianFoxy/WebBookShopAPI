﻿namespace WebBookShopAPI.Data.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageURL { get; set; }
        public List<string> Author { get; set; }
        public List<string> Genre { get; set; }
    }
}
