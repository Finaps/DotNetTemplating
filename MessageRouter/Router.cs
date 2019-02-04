using System;
using communication.Rabbit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FormatWith;
using communication.Models;
using communication.ExtensionMethods;
using communication.ResourceRepositories.TemplateRepository;
using System.Threading.Tasks;

namespace communication.MessageRouter
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

      rabbitManager.Subscribe(ReceivedHandler, "Communication");
    }

    ///Rabbit Subscribe function.
    void ReceivedHandler(object sender, BasicDeliverEventArgs ea)
    {
      var body = ea.ParseBodyToObject<CommunicationRecord>();
      RouteMessage(body);
    }
    ///Main Router
    public void RouteMessage(CommunicationRecord record)
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


    async void ManageEmails(CommunicationRecord record)
    {
      var temp = await templateRepository.FindOrGet(record.TemplateId);
      string formated = temp.Content.FormatWith(record.Parameters);
      var mail = new EmailRequest() { Recipient = record.EmailAddress, Content = formated };
      rabbitManager.Publish<EmailRequest>("communication.send.email", mail);
    }

    void ManageSMS(CommunicationRecord record)
    {

    }

  }
}