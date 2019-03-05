using System;
using System.Collections.Generic;

namespace MicroService.Common.Rabbit
{
  public class RabbitOptions
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
    public string HostName { get; set; }
    public ExchangeOptions[] Exchanges { get; set; }
    public QueueOptions[] Queues { get; set; } = new List<QueueOptions>().ToArray();
  }

  public class ExchangeOptions
  {
    public string Exchange { get; set; }
    public string Type { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; }
  }

  public class QueueOptions
  {
    public string Name { get; set; }
    public string Exchange { get; set; }
    public string RoutingKey { get; set; }
    public bool Exclusive { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; }
  }

}
