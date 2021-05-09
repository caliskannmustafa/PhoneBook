using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class ContactInfoDisplayModel
    {
        public int Id { get; set; }
        public string ContactType { get; set; }
        public string Value { get; set; }
    }
}
