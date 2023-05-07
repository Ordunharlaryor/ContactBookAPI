using AutoMapper;
using ContactBook.Core.Abstraction.Interfaces;
using ContactBook.Core.Utilities;
using ContactBook.Domain.Models;
using ContactBook.Infrastructure.ContactBookContext;
using Microsoft.EntityFrameworkCore;



namespace ContactBook.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactBook_DbContext _dbContext;
        private readonly IMapper _mapper;
        public ContactRepository(ContactBook_DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;

        }

        public async Task<PagedResult<ContactsModel>> GetContactsAsync(int pageNumber, int pageSize)
        {
            var pagedResult = new PagedResult<ContactsModel>();
            var query = _dbContext.Contacts.AsQueryable();

            pagedResult.TotalCount = await query.CountAsync();
            pagedResult.PageSize = pageSize;
            pagedResult.PageNumber = pageNumber;

            var skip = (pageNumber - 1) * pageSize;
            pagedResult.Items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return pagedResult;
        }

        public async Task<ContactsModel?> GetContactByIdAsync(int id)
        {
           return await _dbContext.Contacts.FindAsync(id);
          
        }

        public async Task<int> AddContactAsync(ContactsModel contact)
        {
          var ContactsToAdd = await _dbContext.Contacts.AddAsync(contact);

            return await _dbContext.SaveChangesAsync();
        }



        public async Task<int> UpdateContactAsync(ContactsModel contact)
        {
            var existingContact = await _dbContext.Contacts.FindAsync(contact.Id);
            if (existingContact == null)
            {
                return 0;
            }

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.ImageUrl = contact.ImageUrl;

            return await _dbContext.SaveChangesAsync();
        }



        public async Task<int> PatchContactColumnsAsync(ContactsModel contact)
        {
            var existingContact = await _dbContext.Contacts.FindAsync(contact.Id);
            if (existingContact == null) return 0;
           
            existingContact.FirstName = contact.FirstName ?? existingContact.FirstName;
            existingContact.LastName = contact.LastName ?? existingContact.LastName;
            existingContact.Email = contact.Email ?? existingContact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber ?? existingContact.PhoneNumber;
            existingContact.ImageUrl = contact.ImageUrl ?? existingContact.ImageUrl;

            return await _dbContext.SaveChangesAsync();
        }
           
        public async Task<PagedResult<ContactsModel>> SearchContactsAsync(string searchTerm, int pageNumber, int pageSize)
        {
            var pagedResult = new PagedResult<ContactsModel>();
            var query = _dbContext.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.FirstName.Contains(searchTerm) ||
                                         c.LastName.Contains(searchTerm) ||
                                         c.Email.Contains(searchTerm) ||
                                         c.PhoneNumber.Contains(searchTerm));
            }

            pagedResult.TotalCount = await query.CountAsync();
            pagedResult.PageSize = pageSize;
            pagedResult.PageNumber = pageNumber;

            var skip = (pageNumber - 1) * pageSize;
            pagedResult.Items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return pagedResult;
        }


        public async Task<int> DeleteContactAsync(int id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return 0;
            }

            _dbContext.Contacts.Remove(contact);
            return await _dbContext.SaveChangesAsync();
        }
    }
}


