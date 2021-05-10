using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.DataGenerator.Controllers
{
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        [HttpGet]
        [Route("welcome/Get")]
        public string Get()
        {
            return "Welcome to Phonebook.DataGenerator";
        }
    }
}
