using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class ReportCreateViewModel
    {
        public virtual LocationViewModel Location { get; set; }
    }
}
