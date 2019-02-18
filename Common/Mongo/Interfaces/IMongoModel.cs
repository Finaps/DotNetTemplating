using System;
using MongoDB.Bson;

namespace MicroService.Common.Mongo
{
    public interface IMongoModel
    {
        Guid Id { get; set; }
    }
}
