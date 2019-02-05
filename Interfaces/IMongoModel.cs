using System;
using MongoDB.Bson;

namespace MicroService.Interfaces
{
  public interface IMongoModel
  {
    ObjectId Id { get; set; }
  }
}
