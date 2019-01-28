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
    private string FilePath;
  
    public LogWriter(IConfiguration config){
      FilePath = config.GetSection("Logwriter")["FilePath"];
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
      File.AppendAllText("log.txt", sb.ToString());
      sb.Clear();
    }
  }
}