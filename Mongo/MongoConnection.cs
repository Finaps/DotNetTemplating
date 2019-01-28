using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using logging.Interfaces;
using logging.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace logging.Mongo {
  public class MongoConnection {
    public  MongoClient client;
    public  IMongoDatabase database;

    private readonly IConfigurationSection configuration;
    public MongoConnection(IConfiguration config){
      configuration = config.GetSection("Mongo"); //configured via appsettings.json
      Connect();
    }

    private void Connect(){
      client = new MongoClient(configuration["ConnectionString"]);
      database = client.GetDatabase(configuration["Database"]);
    }
  }
}