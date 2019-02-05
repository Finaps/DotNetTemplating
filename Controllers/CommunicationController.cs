using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using communication.Rabbit;
using communication.Mongo;
using communication.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using communication.Interfaces;
using communication.MessageRouter;

namespace communication.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CommunicationController : ControllerBase
  {
    private readonly Router router;
    private IDatabase<CommunicationRecord> database;
    public CommunicationController(Router router, IDatabase<CommunicationRecord> database)
    {
      this.router = router;
      this.database = database;
    }
    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<CommunicationRecord>> Get()
    {
      return database.RetrieveItems();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<CommunicationRecord> Get(string id)
    {
      return database.RetrieveItem(id);
    }

    // POST api/values
    [HttpPost]
    public ActionResult<CommunicationRecord> Post(CommunicationRecord record)
    {
      database.InsertItem(record);
      router.RouteMessage(record);
      return record;
    }
  }
}
