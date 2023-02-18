namespace WebBookShopAPI.Data.Specifications
{
    public class BookSpecParams
    {
        private const int MaxPageSize = 10;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize { get { return _pageSize; } set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; } }

        //string? sort, int? publisherId, int? bookseriesId, string? genres, string? authors
        public string? Sort { get; set; }
        public int? PublisherId { get; set; }
        public int? BookseriesId { get; set; }
        public List<int>? GenreIds { get; set; }
        public List<int>? AuthorIds { get; set; }

        private string? _search;
        public string? Search 
        { 
            get { return _search; }
            set { _search = value.ToLower(); }
        }
    }
}
