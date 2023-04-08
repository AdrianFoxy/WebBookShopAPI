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

        public BookWithAllInfoSpecification(BookSpecParams bookParams)
            : base(x =>
                (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search)) &&
                (!bookParams.PublisherId.HasValue || x.PublisherId == bookParams.PublisherId) &&
                (!bookParams.BookseriesId.HasValue || x.BookSeriesId == bookParams.BookseriesId) &&
                (bookParams.GenreIds == null || bookParams.GenreIds.Count == 0 || x.Genre.Any(x => bookParams.GenreIds.Contains(x.Id))) &&
                (bookParams.AuthorIds == null || bookParams.AuthorIds.Count == 0 || x.Author.Any(x => bookParams.AuthorIds.Contains(x.Id))) &&
                (bookParams.ExceptBookSeriesId == null || !bookParams.ExceptBookSeriesId.Contains(x.BookSeriesId)) &&
                (bookParams.ExceptPublishersId == null || !bookParams.ExceptPublishersId.Contains(x.PublisherId))
            )
        {

            AddInclude(x => x.Publisher);
            AddInclude(x => x.BookSeries);
            AddInclude(x => x.Author);
            AddInclude(x => x.Genre);

            // Add Pagination
            ApplyPaging(bookParams.PageSize * (bookParams.PageIndex - 1), 
                bookParams.PageSize);



            if (!string.IsNullOrEmpty(bookParams.Sort))
            {
                switch (bookParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Title); 
                        break;
                }
            }
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
