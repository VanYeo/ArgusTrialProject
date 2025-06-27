using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Login
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
