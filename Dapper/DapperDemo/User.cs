using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Dapper.Demo;
public record User
{
  public int UserId { get; set; }
  public string Name { get; set; } = null!;
  public int Age { get; set; }

  public int AddressId { get; set; }
  public Address? Address { get; set; }
}
