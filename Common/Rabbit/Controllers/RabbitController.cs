using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroService.Common.Rabbit;
using MicroService.Common.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;

namespace MicroService.Common.Rabbit
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly RabbitManager rabbitManager;
        public RabbitController(RabbitManager rabbit)
        {
            rabbitManager = rabbit;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "nothing he";
        }

        // POST api/values
        [HttpPost]
        public void Post(RabbitMessage value)
        {
            rabbitManager.Publish(value.Key, value.Message);
        }
    }
}
