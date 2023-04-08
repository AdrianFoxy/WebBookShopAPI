﻿namespace WebBookShopAPI.Data.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string UserGenderCode { get; set; }
    }
}
