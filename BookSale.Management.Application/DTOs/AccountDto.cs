using BookSale.Management.Domain.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs
{
    public class AccountDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage ="Role must be not empty")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Username must be not empty")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Username must be between 3 and 50 charaters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fullname must be not empty")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Password must be not empty")]
        [MinLength(3, ErrorMessage = "Password must be greater tha 3 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email must be not empty")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public string? MobilePhone { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.Upload)]
        [ImageValidation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = "Image is invalid")]
        public IFormFile? Avatar{ get; set; }
    }
}
