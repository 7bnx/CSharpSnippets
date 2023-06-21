using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.Microsoft
{
  public interface ILoggerService
  {
    void Write(string message);
  }
}
