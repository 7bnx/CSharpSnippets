using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.MicrosoftDependencyInjection
{
  public interface ILoggerService
  {
    void Write(string message);
  }
}
