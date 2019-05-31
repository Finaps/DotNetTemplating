using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MicroService.Common.Mongo
{
  public class MongoConnection
  {
    public MongoClient client;
    public IMongoDatabase database;
    private readonly MongoOptions configuration;
    public MongoConnection(IOptionsMonitor<MongoOptions> mongoOptions)
    {
      configuration = mongoOptions.CurrentValue;//configured via appsettings.json
      Connect();
    }

    private void Connect()
    {
      client = new MongoClient(configuration.ConnectionString);
      database = client.GetDatabase(configuration.Database);
    }
  }
}
