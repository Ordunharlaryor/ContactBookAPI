using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactBook.Domain.Models;
using ContactBook.Core.DTOs;
using ContactBook.Domain.Enums;
using ContactBook.API.Extensions;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ContactBook.Infrastructure.ContactBookContext;
using Core.ContactBookAPI.Utilities;
using Azure;
using Swashbuckle.AspNetCore.Annotations;
using CloudinaryDotNet.Actions;
using System.Data;

namespace ContactBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UsersModel> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        

        public AuthController(RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config, UserManager<UsersModel> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }


        [HttpPost("register-REGULAR")]
        
        [SwaggerOperation(Summary = "Register as a Regular")]
      
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO model)
        {
            var User = _mapper.Map<RegisterDTO, UsersModel>(model);
            
            var userExists = await _userManager.FindByNameAsync(User.UserName);
            if (userExists != null)
                return BadRequest(ResponseDto<object>.Fail(new List<string> { " User Already exist" }));
        


            UsersModel user = new()
            {
                UserName = User.UserName,
                Email = User.Email,

            };
           
            var result = await _userManager.CreateAsync(user, User.Password);
            if (!result.Succeeded)
                return BadRequest(ResponseDto<object>.Fail(new List<string> { "User Creation failed check user and try again" }));

            if (!await _roleManager.RoleExistsAsync(UsersRoleManager.REGULAR))
                await _roleManager.CreateAsync(new IdentityRole(UsersRoleManager.REGULAR));
           
          
                if (await _roleManager.RoleExistsAsync(UsersRoleManager.REGULAR))
                {
                    await _userManager.AddToRoleAsync(user, UsersRoleManager.REGULAR);
                }
              

            return Ok(ResponseDto<object>.Success(new List<string> { "User created successfully!" }));
        }



        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var Login = _mapper.Map<UsersModel>(model);
            var user = await _userManager.FindByNameAsync(Login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, Login.Password))
            {


                var userRoles = await _userManager.GetRolesAsync(user);


                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("role", userRole));
                    //authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }



                // Generate a symmetric security key from the configuration file
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                // Generate a signing credential using the key
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Set the expiration time for the token
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["Jwt:ExpireDays"]));

                // Create a new JWT token with the user claims, signing credentials, and expiration time
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: authClaims,
                    expires: expires,
                    signingCredentials: creds);

                // Serialize the token to a string
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Return the JWT token to the client
                return Ok(new { token = tokenString });
            }
            return Unauthorized();
        }
    }
}