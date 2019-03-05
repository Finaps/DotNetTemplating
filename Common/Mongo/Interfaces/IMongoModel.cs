using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroService.Common.Mongo
{
  public interface IMongoModel
  {
    [BsonId]
    Guid Id { get; set; }
  }
}
