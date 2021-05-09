using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.ReportListen
{
    public class ReportResultMessage
    {
        public int ReportId { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
