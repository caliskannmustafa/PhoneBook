using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public virtual List<ContactInfoViewModel> ContactInfos { get; set; } = new List<ContactInfoViewModel>();
    }
}
