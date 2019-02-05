using System;
using MicroService.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MicroService.Models {
  public class RabbitMessage{
    
    [JsonProperty("message")]
    public string Message {get;set;}
    public string Key {get;set;}
  }
}