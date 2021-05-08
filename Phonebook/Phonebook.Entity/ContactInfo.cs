using Phonebook.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Entity
{
    public class ContactInfo : BaseEntity
    {
        public EnumContactType ContactType { get; set; }
        public string Value { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
