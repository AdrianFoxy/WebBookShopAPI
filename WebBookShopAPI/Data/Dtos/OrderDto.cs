namespace WebBookShopAPI.Data.Dtos
{
    public class OrderDto
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; } 
        public string BasketId { get; set; }
        public int DeviveryId { get; set; }

    }
}
