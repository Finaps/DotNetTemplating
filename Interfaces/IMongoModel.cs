using System;
using MongoDB.Bson;

namespace microservice.Interfaces
{
  public interface IMongoModel
  {
      ObjectId Id {get;set;}
  }
}