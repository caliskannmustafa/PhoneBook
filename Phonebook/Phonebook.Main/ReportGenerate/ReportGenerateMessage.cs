using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.ReportGenerate
{
    public class ReportGenerateMessage
    {
        public string ReportId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
