using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroService.Common.Rabbit
{
  public class RabbitManager : IMessageQueue //eh how you doin?
  {
    private readonly RabbitConnection rabbitConnection;
    private readonly IConfiguration configuration;
    private readonly ILogger<RabbitManager> logger;
    public RabbitManager(RabbitConnection connection, IConfiguration config, ILogger<RabbitManager> logger)
    {
      rabbitConnection = connection;
      configuration = config;
      this.logger = logger;
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

      logger.LogInformation("Published message : {0} , to key: {1}", body, key);
    }

    public void Subscribe(EventHandler<BasicDeliverEventArgs> receivedEvent, string queue)
    {
      var consume = new EventingBasicConsumer(rabbitConnection.Channel);
      consume.Received += receivedEvent;
      rabbitConnection.Channel.BasicConsume(queue: queue, autoAck: true, consumer: consume);
    }
  }
}
