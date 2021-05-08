using Phonebook.Main.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Person> PersonRepository
        {
            get;
        }
        GenericRepository<ContactInfo> ContactInfoRepository
        {
            get;
        }
        void Save();
    }
}
