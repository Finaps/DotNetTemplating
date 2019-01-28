using System;
using logging.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace logging.Models {
  public class RabbitMessage{
    
    [JsonProperty("message")]
    public string Message {get;set;}
    public string Key {get;set;}
  }
}