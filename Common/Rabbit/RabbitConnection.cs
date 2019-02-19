using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroService.Common.Rabbit
{
  public class RabbitConnection
  {
    public readonly IConnection connection;
    public readonly IModel Channel;

    private readonly RabbitOptions rabbitOptions;
    public RabbitConnection(IConfiguration config, IOptionsMonitor<RabbitOptions> options)
    {
      rabbitOptions = options.CurrentValue;
      connection = Connect();
      Channel = connection.CreateModel();
      SetupExchanges();
      SetupQueues();
    }

    private IConnection Connect()
    {
      var factory = new ConnectionFactory()
      {
        UserName = rabbitOptions.UserName,
        Password = rabbitOptions.Password,
        VirtualHost = rabbitOptions.VirtualHost,
        HostName = rabbitOptions.HostName,
      };
      return factory.CreateConnection();
    }

    private void SetupExchanges()
    {
      foreach (var i in rabbitOptions.Exchanges)
      {
        Channel.ExchangeDeclare(
          exchange: i.Exchange,
          type: i.Type,
          durable: i.Durable,
          autoDelete: i.AutoDelete,
          arguments: null
        );
      }
    }

    private void SetupQueues()
    {
      foreach (var i in rabbitOptions.Queues)
      {
        Channel.QueueDeclare(
          queue: i.Name,
          exclusive: i.Exclusive,
          durable: i.Durable,
          autoDelete: i.AutoDelete
          );

        Channel.QueueBind(
          queue: i.Name,
          exchange: i.Exchange,
          routingKey: i.RoutingKey
        );
      }
    }
  }
}