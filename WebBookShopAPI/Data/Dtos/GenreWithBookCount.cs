using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Dtos
{
    public class GenreWithBookCount
    {
        public Genre Genre { get; set; }
        public int BookCount { get; set; }

    }
}
