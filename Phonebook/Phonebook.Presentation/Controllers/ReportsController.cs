using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Phonebook.Presentation.Communicators;
using Phonebook.Presentation.Models;

namespace Phonebook.Presentation.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApiCommunicator _apiCommunicator;

        public ReportsController(IOptions<AppSettings> appSetting)
        {
            _apiCommunicator = new ApiCommunicator(appSetting.Value.ApiBaseAddress);
        }

        // GET: Reports1
        public async Task<IActionResult> Index()
        {
            return View(_apiCommunicator.GetReportList());
        }

        // GET: Reports1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = _apiCommunicator.GetReportDetail(id.Value);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Latitude,Longitude")] LocationViewModel location)
        {
            if (ModelState.IsValid)
            {
                _apiCommunicator.CreateReport(new ReportCreateViewModel()
                {
                    Location = location
                });
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        public ActionResult GetData()
        {
            var data = _apiCommunicator.GetReportList();
            return View("_PartialView", data);

        }
    }
}
