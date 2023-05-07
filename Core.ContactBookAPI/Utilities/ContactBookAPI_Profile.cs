using AutoMapper;
using ContactBook.Core.DTOs;
using ContactBook.Domain.Models;
using Core.ContactBookAPI.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Core.Utilities
{
    public class ContactBookAPI_Profile : Profile
    {
            public ContactBookAPI_Profile()
            {
            CreateMap<ContactsModel, ContactDTO>().ReverseMap();

            CreateMap<UsersModel, RegisterDTO>().ReverseMap();
            CreateMap<UsersModel, LoginDTO>().ReverseMap();
     
            CreateMap<ContactsModel, UpdateContactDTO>().ReverseMap();
            CreateMap<UsersModel, UserDTO>().ReverseMap();

            CreateMap(typeof(PagedResult<ContactsModel>), typeof(PagedResultDTO<ContactDTO>)).ReverseMap();

            }
        }
    }

