using Phonebook.Presentation.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public EnumReportStatus ReportStatus { get; set; }
        public virtual ReportDetailViewModel ReportDetail { get; set; }
        public virtual LocationViewModel Location { get; set; }
    }
}
