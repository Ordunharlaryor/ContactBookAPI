using ContactBook.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace ContactBook.Domain.Models
{
    public class UsersModel : IdentityUser
    {

       
        public string Password { get; set; } = string.Empty;

    
        public string ComfirmPassword { get; set; } = string.Empty;
        //public string UsersRole { get; set; } = string.Empty;
      
    }
}
