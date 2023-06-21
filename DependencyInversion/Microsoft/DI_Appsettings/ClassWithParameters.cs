using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.DependencyInversion.Microsoft;

interface IParameters
{
  string Key1 { get; set; }
  string Key2 { get; set; }
}

class Parameters : IParameters
{
  public string Key1 { get; set; } = null!;
  public string Key2 { get; set; } = null!;
}

class ClassWithParameters
{
  private readonly IParameters _parameters;
  public ClassWithParameters(IParameters parameters) => _parameters = parameters;
  public void Write() => Console.WriteLine($"Key1: {_parameters.Key1}; Key2: {_parameters.Key2}");
}
