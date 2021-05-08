using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.Controllers
{
    [ApiController]
    public class ContactInfoController : ControllerBase
    {

        [HttpGet]
        [Route("contactinfo/Get/{userId}")]
        public IEnumerable<WeatherForecast> Get(int userId)
        {
            //TODO: Get and Return ContactInfo List By userId
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("contactinfo/Create")]
        public IEnumerable<WeatherForecast> Create()
        {
            //TODO: Create new ContactInfo
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("contactinfo/Update")]
        public IEnumerable<WeatherForecast> Update()
        {
            //TODO: Update ContactInfo
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("contactinfo/Delete/{id}")]
        public IEnumerable<WeatherForecast> Delete(int id)
        {
            //TODO: Delete ContactInfo
            throw new NotImplementedException();
        }
    }
}
