using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.Microsoft
{
  public class SimpleConsoleLoggerService : ILoggerService
  {
    public void Write(string message) => Console.WriteLine(message);
  }
}
