using ContactBook.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ContactBook.Infrastructure.ContactBookContext
{
    public class ContactBook_DbContext : IdentityDbContext<UsersModel>
    {
        public ContactBook_DbContext(DbContextOptions<ContactBook_DbContext> options) : base(options) 
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // modelBuilder.Entity<UsersModel>().ToTable("AspNetUsers");
        }

        public DbSet<ContactsModel> Contacts { get; set; }
       public DbSet<UsersModel> usersModels { get; set; }

    }
}
