using ContactBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactBook.Core.DTOs
{
    public class UserDTO
    {
        [Required]
        public  string? UserName { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string ComfirmPassword { get; set; } = string.Empty;
     
    }

    
}
