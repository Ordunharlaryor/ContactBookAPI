using AutoMapper;
using ContactBook.Core.Abstraction.Interfaces;
using ContactBook.Core.DTOs;
using ContactBook.Core.Utilities;
using ContactBook.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Core.Implementations.Services
{
        public class ContactService : IContactService
        {
            private readonly IContactRepository _contactRepository;
        

            public ContactService(IContactRepository contactRepository)
            {
                _contactRepository = contactRepository;
            }
        public async Task<PagedResult<ContactsModel>> SearchContactsAsync(string searchTerm, int pageNumber, int pageSize)
        {
            return await _contactRepository.SearchContactsAsync(searchTerm, pageNumber, pageSize);
        }


        public async Task<PagedResult<ContactsModel>> GetContactsAsync(int pageNumber, int pageSize)
            {
                return await _contactRepository.GetContactsAsync(pageNumber, pageSize);
            }

           
        public async Task<ContactsModel> GetContactByIdAsync(int id)
            {
                return await _contactRepository.GetContactByIdAsync(id);
            }

        public async Task<int> AddContactAsync(ContactsModel contacts)
        {
            if (string.IsNullOrWhiteSpace(contacts.FirstName) || string.IsNullOrWhiteSpace(contacts.LastName))
            {
                throw new ArgumentException("First name and last name are required.");
            }

            if (!IsValidEmail(contacts.Email))
            {
                throw new ArgumentException("Invalid email address.");
            }

            var contact = new ContactsModel
            {
                FirstName = contacts.FirstName,
                LastName = contacts.LastName,
                Email = contacts.Email,
                PhoneNumber = contacts.PhoneNumber,
                ImageUrl = contacts.ImageUrl
            };

            return await _contactRepository.AddContactAsync(contact);
        }
            private bool IsValidEmail(string email)
            {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public async Task<int> UpdateContactAsync(ContactsModel contact)
        {
            // Check if contact exists
            var existingContact = await _contactRepository.GetContactByIdAsync(contact.Id);
            if (existingContact == null)
            {
                return 0;
            }

            // Update contact fields
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.ImageUrl = contact.ImageUrl;

            // Update contact in database
           var result = await _contactRepository.UpdateContactAsync(existingContact);
            return result;
            
        }


        public async Task<int> PatchContactColumnsAsync(ContactsModel contact)
        {
            var existingContact = await _contactRepository.GetContactByIdAsync(contact.Id);
            if (existingContact == null)
            {
                throw new ArgumentException($"Contact with id {contact.Id} not found");
            }

            if (!string.IsNullOrEmpty(contact.FirstName))
            {
                existingContact.FirstName = contact.FirstName;
            }

            if (!string.IsNullOrEmpty(contact.LastName))
            {
                existingContact.LastName = contact.LastName;
            }

            if (!string.IsNullOrEmpty(contact.Email))
            {
                existingContact.Email = contact.Email;
            }

            if (!string.IsNullOrEmpty(contact.PhoneNumber))
            {
                existingContact.PhoneNumber = contact.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(contact.ImageUrl))
            {
                existingContact.ImageUrl = contact.ImageUrl;
            }

            return await _contactRepository.UpdateContactAsync(existingContact);
        }
    

    public async Task<int> DeleteContactAsync(int id)
            {
                return await _contactRepository.DeleteContactAsync(id);
            }
        }

    }
