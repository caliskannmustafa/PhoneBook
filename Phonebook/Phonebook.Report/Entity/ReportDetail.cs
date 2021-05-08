using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.Entity
{
    public class ReportDetail : BaseEntity
    {
        public int ReportId { get; set; }
        public virtual Report Report { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
        public virtual Location Location { get; set; }
    }
}
