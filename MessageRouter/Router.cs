using System;
using MicroService.Rabbit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FormatWith;
using MicroService.Models;
using MicroService.ExtensionMethods;
using MicroService.ResourceRepositories.TemplateRepository;
using System.Threading.Tasks;

namespace MicroService.MessageRouter
{
  public class Router
  {
    private readonly RabbitManager rabbitManager;
    private readonly IConfiguration configuration;
    private readonly TemplateResourceRepository templateRepository;
    public Router(RabbitManager rabbit, IConfiguration config, TemplateResourceRepository template)
    {
      rabbitManager = rabbit;
      configuration = config;
      templateRepository = template;

      rabbitManager.Subscribe(ReceivedHandler, "MicroService");
    }

    ///Rabbit Subscribe function.
    void ReceivedHandler(object sender, BasicDeliverEventArgs ea)
    {
      var body = ea.ParseBodyToObject<MicroServiceRecord>();
      RouteMessage(body);
    }
    ///Main Router
    public void RouteMessage(MicroServiceRecord record)
    {
      switch (record.Method)
      {
        case "email":
          ManageEmails(record);
          break;
        case "sms":
          ManageSMS(record);
          break;
        default:
          return;
      }
    }


    async void ManageEmails(MicroServiceRecord record)
    {
      var temp = await templateRepository.FindOrGet(record.TemplateId);
      string formated = temp.Content.FormatWith(record.Parameters);
      var mail = new EmailRequest() { Recipient = record.EmailAddress, Content = formated };
      rabbitManager.Publish<EmailRequest>("MicroService.send.email", mail);
    }

    void ManageSMS(MicroServiceRecord record)
    {

    }

  }
}