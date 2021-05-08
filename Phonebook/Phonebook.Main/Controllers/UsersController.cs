using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Main.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("users/Get")]
        public IEnumerable<WeatherForecast> GetList()
        {
            //TODO: Get and Return UserList
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("users/Get/{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            //TODO: Get and Return User
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("users/Create")]
        public IEnumerable<WeatherForecast> Create()
        {
            //TODO: Create new User
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("users/Update")]
        public IEnumerable<WeatherForecast> Update()
        {
            //TODO: Update User
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("users/Delete/{id}")]
        public IEnumerable<WeatherForecast> Delete(int id)
        {
            //TODO: Delete User
            throw new NotImplementedException();
        }
    }
}
