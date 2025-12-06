using MODEL.CommonConfig;
using MODEL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{

    public class RegisterDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = default!;

        [Required]
        [Compare("Password", ErrorMessage = "Password confirmation does not match")]
        public string ConfirmPassword { get; set; } = default!;
    }

    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }

    public class LoginResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public string? Token { get; set; }
        public object? User { get; set; }
    }

    public class RegisterResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public User? User { get; set; }
    }

}
