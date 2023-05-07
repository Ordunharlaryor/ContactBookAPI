using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactBook.Core.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;
       
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
