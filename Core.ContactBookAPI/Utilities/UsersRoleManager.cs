using ContactBook.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ContactBookAPI.Utilities
{
    public static class UsersRoleManager
    {
        public static string ADMIN = "ADMIN";
        public static string REGULAR = "REGULAR";
    }
}
