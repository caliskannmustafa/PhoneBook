using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet]
        [Route("report/Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            //TODO: Get and Return UserList
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("report/Get/{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            //TODO: Get and Return User
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("report/Create/{id}")]
        public IEnumerable<WeatherForecast> Create()
        {
            //TODO: Create new User
            throw new NotImplementedException();
        }
    }
}
