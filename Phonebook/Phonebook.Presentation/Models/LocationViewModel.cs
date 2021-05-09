using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        [Required]
        [Range(typeof(double), "-90", "90")]
        public double Latitude { get; set; }
        [Required]
        [Range(typeof(double), "-180", "180")]
        public double Longitude { get; set; }

    }
}
