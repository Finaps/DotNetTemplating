using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using MicroService.Common.Common;
using MicroService.Common.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroService.Common.Mongo
{
  public class MongoDatabase<T> : IDatabase<T> where T : IMongoModel
  {
    private readonly MongoClient client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<T> collection;
    private readonly MongoConnection connection;
    public MongoDatabase(MongoConnection connection, string collectionName)
    {
      this.connection = connection;
      database = connection.database;
      client = connection.client;
      collection = database.GetCollection<T>(collectionName);
    }

    public T InsertItem(T obj, CancellationToken cancellationToken = default(CancellationToken))
    {
      collection.InsertOne(obj, cancellationToken: cancellationToken);
      return obj;
    }

    public void RemoveItem(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      var toRemove = new Guid(id);
      RemoveItem(toRemove, cancellationToken);
    }

    public void RemoveItem(T obj, CancellationToken cancellationToken = default(CancellationToken))
    {
      RemoveItem(obj.Id, cancellationToken);
    }

    public void RemoveItem(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
      collection.DeleteOne(GetId(id), cancellationToken: cancellationToken);
    }

    public T RetrieveItem(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      return collection.FindSync(GetId(id), cancellationToken: cancellationToken).FirstOrDefault();
    }

    public T RetrieveItem(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
      return collection.FindSync(GetId(id), cancellationToken: cancellationToken).FirstOrDefault();
    }

    public T RetrieveItem(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
    {
      return collection.FindSync(predicate, cancellationToken: cancellationToken).FirstOrDefault();
    }

    public List<T> RetrieveItems(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
    {
      return collection.FindSync(predicate, cancellationToken: cancellationToken).ToList();
    }

    public List<T> RetrieveItems()
    {
      return collection.AsQueryable().ToList();
    }

    public List<T> RetrieveItems(Expression<Func<T, bool>> predicate, int limit, int offset, CancellationToken cancellationToken = default(CancellationToken))
    {
      var options = CreateOptions(limit, offset);
      return collection.FindSync(predicate, options, cancellationToken).ToList();
    }

    public List<T> RetrieveItems(int limit, int offset)
    {
      var options = CreateOptions(limit, offset);
      return collection.AsQueryable().Take(limit).Skip(offset).ToList();
    }

    public T UpdateItem(T obj, string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      obj.Id = new Guid(id);
      return UpdateItem(obj, cancellationToken);
    }

    public T UpdateItem(T obj, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (obj.Id == default(Guid))
        return default(T);

      return collection.FindOneAndReplace(GetId(obj.Id), obj, cancellationToken: cancellationToken);
    }

    public long Count(CancellationToken cancellationToken = default(CancellationToken))
    {
      return Count(new BsonDocument(), cancellationToken);
    }

    public long Count(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
    {
      var options = Builders<T>.Filter.Where(predicate);
      return Count(options, cancellationToken);
    }

    public long Count(FilterDefinition<T> options, CancellationToken cancellationToken = default(CancellationToken))
    {
      return collection.CountDocuments(options, cancellationToken: cancellationToken);
    }

    private FindOptions<T> CreateOptions(int limit, int offset)
    {
      return new FindOptions<T>
      {
        Limit = limit,
        Skip = offset,
      };
    }

    private FilterDefinition<T> GetId(string id)
    {
      return GetId(new Guid(id));
    }

    private FilterDefinition<T> GetId(Guid id)
    {
      return Builders<T>.Filter.Eq(x => x.Id, id);
    }
  }
}
