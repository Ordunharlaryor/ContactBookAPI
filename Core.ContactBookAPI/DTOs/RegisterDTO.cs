using CloudinaryDotNet.Actions;
using ContactBook.Core.DTOs;
using ContactBook.Domain.Enums;
using ContactBook.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactBook.Core.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
       
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ComfirmPassword { get; set; } = string.Empty;
       
      //  public string  UsersRole { get; set; } = string.Empty;   

    }
}
