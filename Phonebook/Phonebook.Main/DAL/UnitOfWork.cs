using Microsoft.Extensions.Configuration;
using Phonebook.Main.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IConfiguration _configuration;
        private readonly PhonebookDbContext _context;

        public UnitOfWork(PhonebookDbContext context)
        {
            _context = context;
        }

        private GenericRepository<Person> personRepository;
        private GenericRepository<ContactInfo> contactInfoRepository;

        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (this.personRepository == null)
                {
                    this.personRepository = new GenericRepository<Person>(_context);
                }
                return personRepository;
            }
        }

        public GenericRepository<ContactInfo> ContactInfoRepository
        {
            get
            {

                if (this.contactInfoRepository == null)
                {
                    this.contactInfoRepository = new GenericRepository<ContactInfo>(_context);
                }
                return contactInfoRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
