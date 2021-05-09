using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class ReportDetailViewModel
    {
        public int ReportId { get; set; }
        public virtual ReportViewModel Report { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
