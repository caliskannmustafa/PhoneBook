using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.Entity
{
    public class Location
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int ReportDetailId { get; set; }
        public virtual ReportDetail ReportDetail { get; set; }
    }
}
