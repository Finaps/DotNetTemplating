using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using logging.Rabbit;
using logging.Mongo;
using logging.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using logging.Interfaces;
using System.Text;
using RabbitMQ.Client;
using System.IO;

namespace logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogWriter logger;
        public LoggingController(ILogWriter logger)
        {
          this.logger = logger;
        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var FileName = String.Format("{0}.txt", DateTime.UtcNow.ToString("ddmmyyyy"));
            var file = System.IO.File.ReadAllBytes(FileName);
            return File(file, "application/force-download", "log.txt");
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
        }
    }
}
