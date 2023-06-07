using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Specifications
{
    public class BookWithFiltersCountSpecification : BaseSpecification<Book>
    {
        public BookWithFiltersCountSpecification(BookSpecParams bookParams) : base(x =>
                (string.IsNullOrEmpty(bookParams.Search) || x.Title.ToLower().Contains(bookParams.Search)) &&
                (bookParams.PublishersIds == null || bookParams.PublishersIds.Count == 0 || bookParams.PublishersIds.Contains(x.PublisherId)) &&
                (bookParams.BookseriesIds == null || bookParams.BookseriesIds.Count == 0 || bookParams.BookseriesIds.Contains(x.BookSeriesId)) &&
                (bookParams.GenreIds == null || bookParams.GenreIds.Count == 0 || x.Genre.Any(x => bookParams.GenreIds.Contains(x.Id))) &&
                (bookParams.AuthorIds == null || bookParams.AuthorIds.Count == 0 || x.Author.Any(x => bookParams.AuthorIds.Contains(x.Id))) &&
                (bookParams.ExceptBookSeriesId == null || !bookParams.ExceptBookSeriesId.Contains(x.BookSeriesId)) &&
                (bookParams.ExceptPublishersId == null || !bookParams.ExceptPublishersId.Contains(x.PublisherId)) &&
                (bookParams.ExceptAuthorIds == null || bookParams.ExceptAuthorIds.Count == 0 || !x.Author.Any(a => bookParams.ExceptAuthorIds.Contains(a.Id))) &&
                (bookParams.ExceptGenresIds == null || bookParams.ExceptGenresIds.Count == 0 || !x.Genre.Any(a => bookParams.ExceptGenresIds.Contains(a.Id))) &&
                (bookParams.MinUploadDate == null || x.UploadedInfo >= bookParams.MinUploadDate) &&
                (bookParams.MaxUploadDate == null || x.UploadedInfo <= bookParams.MaxUploadDate)
            )
        { 
        }
    }
}
