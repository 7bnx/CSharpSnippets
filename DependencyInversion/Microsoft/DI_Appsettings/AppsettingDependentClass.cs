using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.Microsoft;

public interface IAppsettingDependentClass
{
  void Write();
}

public class AppsettingDependentDebug : IAppsettingDependentClass
{
  public void Write() => Console.WriteLine(nameof(AppsettingDependentDebug));
}

public class AppsettingDependentRelease : IAppsettingDependentClass
{
  public void Write() => Console.WriteLine(nameof(AppsettingDependentRelease));
}
