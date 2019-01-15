using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microservice.Rabbit;
using microservice.Mongo;
using microservice.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using microservice.Interfaces;

namespace microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtorController : ControllerBase
    {
        private IDatabase<Debtor> database;
        public DebtorController(IDatabase<Debtor> database)
        {
            this.database = database;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Debtor>> Get()
        {
            return database.RetrieveItems();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public  ActionResult<Debtor> Get(string id)
        {
            return database.RetrieveItem(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Debtor> Post(Debtor value)
        {
            return database.InsertItem(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Debtor> Put(string id, Debtor value)
        {
            return database.UpdateItem(value, id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            database.RemoveItem(id);
        }
    }
}
