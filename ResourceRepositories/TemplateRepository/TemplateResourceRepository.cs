using System;
using System.Threading.Tasks;
using communication.Interfaces;
using communication.Models;
using communication.Rabbit;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using communication.ExtensionMethods;

namespace communication.ResourceRepositories.TemplateRepository
{
  public class TemplateResourceRepository : IResourceRepository<Template>
  {
    private readonly IDatabase<Template> database;
    private readonly RabbitManager rabbitManager;
    private readonly IRestClient restClient;
    public TemplateResourceRepository(IDatabase<Template> db, RabbitManager manager, IRestClient rest)
    {
      database = db;
      rabbitManager = manager;
      restClient = rest;
      rabbitManager.Subscribe(TemplateSubscriber, "Template");
    }
    public void ClearStore()
    {

    }

    public async ValueTask<Template> FindOrGet(string id)
    {
      var dbCheck = database.RetrieveItem(id);
      if (!dbCheck.Equals(default(Template)))
        return dbCheck;

      return await RequestExternally(id);
    }

    private async Task<Template> RequestExternally(string id)
    {
      try
      {
        var result = await restClient.GetAsync<Template>("http://www.givemethetemplate/" + id);
        database.InsertItem(result);
        return result;
      }
      catch (Exception e)
      {
        Console.WriteLine("prob http error");
        return null;
      }
    }

    private void TemplateSubscriber(Object sender, BasicDeliverEventArgs ea)
    {
      var operation = ea.RoutingKey.Split(".")[1].ToLower();
      switch (operation)
      {
        case "create":
          var template = ea.ParseBodyToObject<Template>();
          database.InsertItem(template);
          break;
        default:
          return;
      }
    }
  }
}