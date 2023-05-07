using ContactBook.Core.Utilities;
using ContactBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Core.Abstraction.Interfaces
{
    public interface IContactService
    {
        Task<PagedResult<ContactsModel>> SearchContactsAsync(string searchTerm, int pageNumber, int pageSize);
        Task<PagedResult<ContactsModel>> GetContactsAsync(int pageNumber, int pageSize);
        Task<ContactsModel> GetContactByIdAsync(int id);
        Task<int> AddContactAsync(ContactsModel contacts);
        Task<int> UpdateContactAsync(ContactsModel contact);
        Task<int> PatchContactColumnsAsync(ContactsModel contact);
        Task<int> DeleteContactAsync(int id);
    }
}
