using System;
using System.IO;
using System.Text;
using logging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace logging.Logger
{
  public class LogWriter : ILogWriter
  {
    StringBuilder sb = new StringBuilder();
  
    public LogWriter(IConfiguration config){
    }
    public void WriteErrorToLog(ILogMessage message)
    {
      sb.AppendFormat("{0} - Error: {1}", DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"), message.Title);
      sb.Append(Environment.NewLine);
      sb.Append(message.Message);
      sb.Append(Environment.NewLine);
      sb.Append(message.Stacktrace);

      WriteMessageToLog();
    }

    public void WriteInformationToLog(ILogMessage message)
    {
      throw new NotImplementedException();
    }

    public void WriteWarningToLog(ILogMessage message)
    {
      throw new NotImplementedException();
    }

    private void WriteMessageToLog()
    {
      var FileName = String.Format("{0}.txt", DateTime.UtcNow.ToString("ddmmyyyy"));
      File.AppendAllText(FileName, sb.ToString());
      sb.Clear();
    }
  }
}