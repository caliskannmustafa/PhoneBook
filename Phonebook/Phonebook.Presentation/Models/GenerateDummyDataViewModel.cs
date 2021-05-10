using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Models
{
    public class GenerateDummyDataViewModel
    {
        [Required]
        [Range(typeof(int), "1", "10000")]
        public int DummyDataCount { get; set; }
    }
}
