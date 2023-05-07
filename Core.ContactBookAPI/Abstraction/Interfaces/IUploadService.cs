using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ContactBookAPI.Implementations.Services
{
    public interface IUploadService
    {
        Task<Dictionary<string, string>> UploadFileAsync(IFormFile file);
    }
}
