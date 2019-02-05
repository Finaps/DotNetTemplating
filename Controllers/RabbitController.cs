using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroService.Rabbit;
using MicroService.Mongo;
using MicroService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using MicroService.Interfaces;
using System.Text;
using RabbitMQ.Client;

namespace MicroService.Controllers
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

        // GET api/values/5
        // [HttpGet("{id}")]
        // public  ActionResult<Debtor> Get(string id)
        // {
        //     return database.RetrieveItem(id);
        // }

        // POST api/values
        [HttpPost]
        public void Post(RabbitMessage value)
        {
          rabbitManager.Publish(value.Key, value.Message);
        }
    }
}
