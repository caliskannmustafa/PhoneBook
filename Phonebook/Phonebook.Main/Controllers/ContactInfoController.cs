using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Main.DAL;
using Phonebook.Main.Entity;
using Phonebook.Main.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.Controllers
{
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("contactinfo/Get/{personId}")]
        public IEnumerable<ContactInfo> Get(int personId)
        {
            GeoCoordinatePortable.GeoCoordinate firstCoordinate = new GeoCoordinatePortable.GeoCoordinate(41.487999, 32.371090);
            GeoCoordinatePortable.GeoCoordinate secondCoordinate = new GeoCoordinatePortable.GeoCoordinate(39.807590, 32.696690);

            return _unitOfWork.ContactInfoRepository.Get(t => t.PersonId == personId).ToList();
        }

        [HttpGet]
        [Route("contactinfo/Get/{personId}/{contactType}")]
        public IEnumerable<ContactInfo> Get(int personId, EnumContactType contactType)
        {
            return _unitOfWork.ContactInfoRepository.Get(t => t.PersonId == personId).Where(t => t.ContactType == contactType).ToList();
        }

        [HttpPost]
        [Route("contactinfo/Create")]
        public int Create(ContactInfo contactInfo)
        {
            contactInfo.CreateDate = DateTime.Now;
            _unitOfWork.ContactInfoRepository.Insert(contactInfo);
            _unitOfWork.Save();
            return contactInfo.Id;
        }

        [HttpGet]
        [Route("contactinfo/Delete/{id}")]
        public void Delete(int id)
        {
            _unitOfWork.ContactInfoRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
