using System;
using logging.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace logging.Models {
  public class Debtor : IMongoModel{
    public ObjectId Id {get;set;}
    
    [JsonProperty("name")]
    public string Name {get;set;}
    public float HowmuchHeOw {get;set;} = 900000000;

  }
}