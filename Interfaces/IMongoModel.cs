using System;
using MongoDB.Bson;

namespace communication.Interfaces
{
  public interface IMongoModel
  {
      ObjectId Id {get;set;}
  }
}