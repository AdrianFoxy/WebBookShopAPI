namespace WebBookShopAPI.Data.Dtos
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserGenderCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
