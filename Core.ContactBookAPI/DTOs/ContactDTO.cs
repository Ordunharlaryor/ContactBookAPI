using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Core.DTOs
{
    public class ContactDTO
    {
            public string? FirstName { get; set; } = null!;
            public string? LastName { get; set; } = null!;
            public string? Email { get; set; } = null!;
            public string? PhoneNumber { get; set; } = null!;
            public string UserId { get; set; } = string.Empty;
            public string? ImageUrl { get; set; } = null!;
    }
}
