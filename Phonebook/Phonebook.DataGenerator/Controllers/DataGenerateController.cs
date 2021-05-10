using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Phonebook.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.DataGenerator.Controllers
{
    [ApiController]
    public class DataGenerateController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public DataGenerateController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        [Route("datagenerate/Get/{count}")]
        public string Get(int count)
        {
            _eventBus.PushMessage("data_generate", JsonConvert.SerializeObject(count));

            return "Data Generation Has Started";
        }
    }
}
