using System;
using System.Text;
using communication.Interfaces;
using communication.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace communication.Rabbit
{
  public class RabbitManager : IMessageQueue //eh how you doin?
  {
    private readonly RabbitConnection rabbitConnection;
    private readonly IConfiguration configuration;
    public RabbitManager(RabbitConnection connection, IConfiguration config)
    {
      rabbitConnection = connection;
      configuration = config;
      //Sub();
    }

    public void Publish<T>(string key, T body)
    {
      var converted = JsonConvert.SerializeObject(body).ToString();
      Publish(key, converted);
    }
    public void Publish(string key, string body)
    {
      var message = Encoding.UTF8.GetBytes(body);
      rabbitConnection.Channel.BasicPublish(
        exchange: "debtor",
        routingKey: key,
        body: message);

      Console.WriteLine("Published message : {0} , to key: {1}", body, key);
    }

    public void Subscribe(EventHandler<BasicDeliverEventArgs> receivedEvent, string queue)
    {
      var consume = new EventingBasicConsumer(rabbitConnection.Channel);
      consume.Received += receivedEvent;
      rabbitConnection.Channel.BasicConsume(queue: queue, autoAck: true, consumer: consume);
    }
    private void Sub()
    {
      var consume = new EventingBasicConsumer(rabbitConnection.Channel);
      consume.Received += (model, ea) =>
      {
        var body = ea.Body;
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        var service = routingKey.Split(".")[0];
        Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
      };
      rabbitConnection.Channel.BasicConsume(queue: "Error", autoAck: true, consumer: consume);
    }
  }
}