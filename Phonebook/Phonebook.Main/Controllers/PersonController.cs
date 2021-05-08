﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Main.DAL;
using Phonebook.Main.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("person/Get")]
        public IEnumerable<Person> Get()
        {
            return _unitOfWork.PersonRepository.Get();
        }

        [HttpGet]
        [Route("person/Get/{id}")]
        public Person Get(int id)
        {
            return _unitOfWork.PersonRepository.GetByID(id);
        }

        [HttpPost]
        [Route("person/Create")]
        public int Create(Person person)
        {
            person.CreateDate = DateTime.Now;
            if (person.ContactInfos != null && person.ContactInfos.Any())
            {
                person.ContactInfos.ForEach(t => t.CreateDate = DateTime.Now);
            }
            _unitOfWork.PersonRepository.Insert(person);
            _unitOfWork.Save();
            return person.Id;
        }

        [HttpPost]
        [Route("person/Update")]
        public void Update(Person person)
        {
            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.Save();
        }

        [HttpGet]
        [Route("person/Delete/{id}")]
        public void Delete(int id)
        {
            _unitOfWork.PersonRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
