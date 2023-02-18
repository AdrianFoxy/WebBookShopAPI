using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Specifications
{
    public class BookWithFiltersCountSpecification : BaseSpecification<Book>
    {
        public BookWithFiltersCountSpecification(BookSpecParams bookParams) : base(x =>
                (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search)) &&
                (!bookParams.PublisherId.HasValue || x.PublisherId == bookParams.PublisherId) &&
                (!bookParams.BookseriesId.HasValue || x.BookSeriesId == bookParams.BookseriesId) &&
                (bookParams.GenreIds == null || bookParams.GenreIds.Count == 0 || x.Genre.Any(x => bookParams.GenreIds.Contains(x.Id))) &&
                (bookParams.AuthorIds == null || bookParams.AuthorIds.Count == 0 || x.Author.Any(x => bookParams.AuthorIds.Contains(x.Id)))
            )
        { 
        }
    }
}
