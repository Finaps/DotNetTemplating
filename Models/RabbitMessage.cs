using System;
using microservice.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace microservice.Models {
  public class RabbitMessage{
    
    [JsonProperty("message")]
    public string Message {get;set;}
    public string Key {get;set;}
  }
}