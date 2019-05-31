using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MicroService.Common.Rabbit
{
  public class RabbitMessage
  {

    [JsonProperty("message")]
    public string Message { get; set; }
    public string Key { get; set; }
  }
}
