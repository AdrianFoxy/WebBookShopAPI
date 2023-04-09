using System.ComponentModel.DataAnnotations;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage ="Пароль має містити 1 заглавну букву, 1 нижнього регістру, 1 число, 1 спеціальний символ, довжина пароля має бути між 6-10 символами"),]
        // This regular expression match can be used for validating strong password. It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character
        // and the length should be between 6-10 characters. The sequence of the characters is not important. This expression follows the above 4 norms specified by
        // microsoft for a strong password. RE from https://regexlib.com/Search.aspx?k=password
        public string Password { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserGenderCode { get; set; }
    }
}
