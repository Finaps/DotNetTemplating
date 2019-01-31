using System;
using communication.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace communication.Models {
  public class RabbitMessage{
    
    [JsonProperty("message")]
    public string Message {get;set;}
    public string Key {get;set;}
  }
}