using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace microservice.Rabbit{
  public class RabbitConnection
  {
    public readonly IConnection connection;
    public readonly IModel Channel;
    private readonly IConfigurationSection configuration;
    private readonly IConfigurationSection exchanges;
    public RabbitConnection(IConfiguration config){
      configuration = config.GetSection("Rabbit"); //configured via appsettings.json
      exchanges = config.GetSection("RabbitExchanges");
      connection = Connect();
      Channel = connection.CreateModel();
      SetupExchanges();
    }

    private IConnection Connect(){
      var factory = new ConnectionFactory()
      {
        UserName = configuration["UserName"],
        Password = configuration["Password"],
        VirtualHost = configuration["VirtualHost"],
        HostName = configuration["HostName"],
      };
       return factory.CreateConnection();
    }

    private void SetupExchanges(){
      foreach(var i in exchanges.GetChildren()){
        Channel.ExchangeDeclare(
          exchange: i["Exchange"],
          type: i["Type"],
          durable: bool.Parse(i["durable"]),
          autoDelete: bool.Parse(i["autoDelete"]),
          arguments: null
        );
      }
    }

    private void SetupReceiving(){
      var channel = connection.CreateModel();
      
        channel.QueueDeclare(queue:"hello",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (ob, ea) => {
           var body = ea.Body;
          var message = Encoding.UTF8.GetString(body);
          Console.WriteLine("Reciever: RECEIVED: {0}", message);
        };

        channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
      }
    

  }
}