using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.Microsoft
{
  public class Logger
  {
    ILoggerService? _loggerService;
    int _loggerId;
    static int _loggersCount = 0;
    public Logger(ILoggerService? loggerService)
      => (_loggerService, _loggerId) = (loggerService, ++_loggersCount);
    public void Log(string message) 
      => _loggerService?.Write($"{message}\t| Logger id: {_loggerId}");
  }
}
