using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Dapper.Demo;
public record Address
{
  public int AddressId { get; set; }
  public string Name { get; set; } = null!;
}
