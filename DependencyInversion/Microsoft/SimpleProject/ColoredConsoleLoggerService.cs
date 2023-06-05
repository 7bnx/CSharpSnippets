using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.Microsoft
{
  public class ColoredConsoleLoggerService : ILoggerService
  {
    public void Write(string message)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(message);
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
