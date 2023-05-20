namespace WebBookShopAPI.Data.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 10;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize { get { return _pageSize; } set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; } }

    }
}
