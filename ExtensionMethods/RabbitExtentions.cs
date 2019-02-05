using System;
using System.Text;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;

namespace MicroService.ExtensionMethods
{
  public static class RabbitExtentions
  {
    public static string BodyAsString(this BasicDeliverEventArgs ea)
    {
      return Encoding.UTF8.GetString(ea.Body);
    }

    public static T ParseBodyToObject<T>(this BasicDeliverEventArgs ea)
    {
      var body = ea.BodyAsString();
      return JToken.Parse(body).ToObject<T>();
    }
  }
}