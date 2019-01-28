using System;
using System.Collections.Generic;

using System.Linq.Expressions;

namespace logging.Interfaces
{
    public interface ILogMessage
    {
      string Title {get;set;}
      string Message {get;set;}
      string Service {get;set;}
      string Stacktrace {get;set;}
      void Log();
    }
}
