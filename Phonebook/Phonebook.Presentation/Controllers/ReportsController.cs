using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            //TODO: Get And Return Report List From Gateway
            return View();
        }
    }
}
