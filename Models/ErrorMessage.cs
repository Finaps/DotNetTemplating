using System;
using logging.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace logging.Models {
  public class ErrorMessage : ILogMessage
  {
    public string Title {get;set;}
    public string Message {get;set;}
    public string Service {get;set;}
    public string Stacktrace {get;set;}

    public void Log()
    {
      Console.BackgroundColor = ConsoleColor.Red;
      Console.ForegroundColor = ConsoleColor.White;

      Console.WriteLine("Error - {0}: {1}", Title, Message);
      Console.ResetColor();
    }
  }
}