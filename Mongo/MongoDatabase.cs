using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using microservice.Interfaces;
using microservice.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace microservice.Mongo {
  public class MongoDatabase<T> : IDatabase<T> where T : IMongoModel{
    private readonly MongoClient client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<T> collection;
    private readonly MongoConnection connection;
    public MongoDatabase(MongoConnection connection, string collectionName){
      this.connection = connection;
      database = connection.database;
      client = connection.client;
      collection = database.GetCollection<T>(collectionName);
    }

    public T InsertItem(T obj)
    {
      collection.InsertOne(obj);
      return obj;
    }

    public void RemoveItem(string id)
    {
      collection.DeleteOne(GetId(id));
    }

    public T RetrieveItem(string id)
    {
      return collection.FindSync(GetId(id)).FirstOrDefault();
    }

    public T RetrieveItem(Expression<Func<T, bool>> predicate)
    {
      return collection.FindSync(predicate).FirstOrDefault();
    }

    public List<T> RetrieveItems(Expression<Func<T, bool>> predicate)
    {
      return collection.FindSync(predicate).ToList();
    }

    public List<T> RetrieveItems()
    {
      return collection.AsQueryable().ToList();
    }

    public T UpdateItem(T obj, string id)
    {
      obj.Id = new ObjectId(id);
      collection.ReplaceOne(GetId(id), obj);
      return obj;
    }

    private FilterDefinition<T> GetId(string id)
    {
      return Builders<T>.Filter.Eq(x => x.Id, new ObjectId(id));
    }
  }
}