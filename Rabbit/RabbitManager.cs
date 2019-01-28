using System;
using System.Text;
using logging.Interfaces;
using logging.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace logging.Rabbit{
  public class RabbitManager : IMessageQueue //eh how you doin?
  {
    private readonly RabbitConnection rabbitConnection;
    private readonly IConfiguration configuration;
    private readonly ILogWriter logger;
    public RabbitManager(RabbitConnection connection, IConfiguration config, ILogWriter logger){
      rabbitConnection = connection;
      configuration = config;
      this.logger = logger;
      Sub();
    }

    public void Publish<T>(string key, T body){
      var converted = JsonConvert.SerializeObject(body).ToString();
      Publish(key, converted);
    }
    public void Publish(string key, string body){
      var message = Encoding.UTF8.GetBytes(body);
      rabbitConnection.Channel.BasicPublish(
        exchange: "debtor",
        routingKey: key,
        body: message);
      
      Console.WriteLine("Published message : {0} , to key: {1}", body, key);
    }

    private void Sub(){
      var consume = new EventingBasicConsumer(rabbitConnection.Channel);
      consume.Received += (model, ea) =>
      {
        var body = ea.Body;
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        var service = routingKey.Split(".")[0];
        var logmessage = new ErrorMessage(){Message = message, Title = service + " error", Service = service};
        logger.WriteErrorToLog(logmessage);
        Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
      };
      rabbitConnection.Channel.BasicConsume(queue: "Error", autoAck: true, consumer: consume);
    }
  }
}