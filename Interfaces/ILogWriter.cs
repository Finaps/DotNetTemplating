using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace logging.Interfaces
{
    public interface ILogWriter
    {
      void WriteErrorToLog(ILogMessage message);
      void WriteWarningToLog(ILogMessage message);
      void WriteInformationToLog(ILogMessage message);
    }
}
