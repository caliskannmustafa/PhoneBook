using Phonebook.Main.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.Entity
{
    public class ContactInfo : BaseEntity
    {
        public EnumContactType ContactType { get; set; }
        public string Value { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
