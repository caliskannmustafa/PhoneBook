using Phonebook.Presentation.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class ContactInfoViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public EnumContactType ContactType { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
