using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace communication.Rabbit
{
  public class RabbitConnection
  {
    public readonly IConnection connection;
    public readonly IModel Channel;
    private readonly IConfigurationSection configuration;
    private readonly IConfigurationSection exchanges;
    private readonly IConfigurationSection queues;
    public RabbitConnection(IConfiguration config)
    {
      configuration = config.GetSection("Rabbit"); //configured via appsettings.json
      exchanges = config.GetSection("RabbitExchanges");
      queues = config.GetSection("RabbitQueues");
      connection = Connect();
      Channel = connection.CreateModel();
      SetupExchanges();
      SetupQueues();
    }

    private IConnection Connect()
    {
      var factory = new ConnectionFactory()
      {
        UserName = configuration["UserName"],
        Password = configuration["Password"],
        VirtualHost = configuration["VirtualHost"],
        HostName = configuration["HostName"],
      };
      return factory.CreateConnection();
    }

    private void SetupExchanges()
    {
      foreach (var i in exchanges.GetChildren())
      {
        Channel.ExchangeDeclare(
          exchange: i["Exchange"],
          type: i["Type"],
          durable: bool.Parse(i["durable"]),
          autoDelete: bool.Parse(i["autoDelete"]),
          arguments: null
        );
      }
    }

    private void SetupQueues()
    {
      foreach (var i in queues.GetChildren())
      {
        Channel.QueueDeclare(queue: i["Name"]);
        Channel.QueueBind(queue: i["Name"], exchange: i["Exchange"], routingKey: i["RoutingKey"]);
      }
    }

  }
}