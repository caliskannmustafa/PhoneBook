using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Phonebook.Presentation.Communicators;
using Phonebook.Presentation.Models;
using Phonebook.Presentation.Models.Enums;

namespace Phonebook.Presentation.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApiCommunicator _apiCommunicator;

        public PeopleController(IOptions<AppSettings> appSettings)
        {
            _apiCommunicator = new ApiCommunicator(appSettings.Value.ApiBaseAddress);
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(_apiCommunicator.GetPeopleList());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _apiCommunicator.GetPerson(id.Value);
            var contactInfos = ContactInfoMapper(person.ContactInfos);
            ViewBag.ContactInfos = contactInfos;

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,CompanyName,Id,CreateDate")] PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                int id = _apiCommunicator.CreatePerson(person);
                return RedirectToAction(nameof(Edit), new { id });
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _apiCommunicator.GetPerson(id.Value);
            var contactInfos = ContactInfoMapper(person.ContactInfos);
            ViewBag.ContactInfos = contactInfos;
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,CompanyName,Id,CreateDate")] PersonViewModel person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _apiCommunicator.UpdatePerson(person);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _apiCommunicator.GetPerson(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _apiCommunicator.DeletePerson(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> CreateContact(int personId)
        {
            ViewBag.PersonId = personId;
            ViewBag.ContactType = new SelectList(Enum.GetValues(typeof(EnumContactType)));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContact(int id, [Bind("ContactType,Value")] ContactInfoViewModel contactInfo)
        {
            if (ModelState.IsValid)
            {
                contactInfo.PersonId = id;
                _apiCommunicator.CreateContactInfo(contactInfo);
                return RedirectToAction(nameof(Edit), new { id });
            }

            return RedirectToAction(nameof(CreateContact), new { personId = id });
        }

        public async Task<IActionResult> DeleteContact(int id, int personId)
        {
            _apiCommunicator.DeleteContactInfo(id);
            return RedirectToAction(nameof(Edit), new { id = personId });
        }

        private List<ContactInfoDisplayModel> ContactInfoMapper(List<ContactInfoViewModel> model)
        {
            List<ContactInfoDisplayModel> contactInfos = new List<ContactInfoDisplayModel>();

            if (model != null && model.Any())
            {
                foreach (var item in model)
                {
                    string contactType = "";
                    switch (item.ContactType)
                    {
                        case EnumContactType.PhoneType:
                            contactType = "Phone";
                            break;
                        case EnumContactType.EmailAddress:
                            contactType = "Email";
                            break;
                        case EnumContactType.GeoLocation:
                            contactType = "Lokasyon";
                            break;
                    }

                    contactInfos.Add(new ContactInfoDisplayModel()
                    {
                        ContactType = contactType,
                        Value = item.Value,
                        Id = item.Id
                    });
                }
            }

            return contactInfos;
        }
    }
}
