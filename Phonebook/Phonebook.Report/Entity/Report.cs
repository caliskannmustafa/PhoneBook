using Phonebook.Report.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.Entity
{
    public class Report : BaseEntity
    {
        public DateTime ReportDate { get; set; }
        public EnumReportStatus ReportStatus { get; set; }
        public virtual ReportDetail ReportDetail { get; set; }
        public virtual Location Location { get; set; }
    }
}
