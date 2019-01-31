using System;
using communication.Rabbit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FormatWith;
using communication.Models;

namespace communication.MessageRouter
{
  public class Router
  {
    private readonly RabbitManager rabbitManager;
    private readonly IConfiguration configuration;
    public Router(RabbitManager rabbit, IConfiguration config)
    {
      rabbitManager = rabbit;
      configuration = config;

      rabbitManager.Subscribe(ReceivedHandler, "Communication");
    }

    void ReceivedHandler(object sender, BasicDeliverEventArgs ea)
    {
      var method = ea.RoutingKey.Split(".")[2].ToLower();
      switch (method)
      {
        case "email":
          ManageEmails(ea);
          break;
        case "sms":
          ManageSMS(ea);
          break;
        default:
          return;
      }
    }
    void ManageEmails(BasicDeliverEventArgs ea)
    {
      string template = configuration.GetSection("TestTemplate").GetSection("Template").Value;
      string formated = template.FormatWith(new { name = "me", content = "jejeje" });
      var mail = new EmailRequest() { Recipient = "me@me.com", Content = formated };
      rabbitManager.Publish<EmailRequest>("communication.send.email", mail);
    }

    void ManageSMS(BasicDeliverEventArgs ea)
    {
    }

  }
}