using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Dtos
{
    public class AuthorWithBookCount
    {
        public Author Author { get; set; }
        public int BookCount { get; set; }
    }
}
