using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Specifications
{
    public class BookWithAllInfoSpecification : BaseSpecification<Book>
    {
        public BookWithAllInfoSpecification() 
        {
            AddInclude(x => x.Publisher);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
        }

        public BookWithAllInfoSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Publisher);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);
        }
    }
}
