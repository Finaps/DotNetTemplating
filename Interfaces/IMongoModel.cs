using System;
using MongoDB.Bson;

namespace logging.Interfaces
{
  public interface IMongoModel
  {
      ObjectId Id {get;set;}
  }
}