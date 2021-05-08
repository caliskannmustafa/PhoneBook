using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Entity
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
    }
}
