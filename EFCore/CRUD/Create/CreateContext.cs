using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.EFCore.Create;

public class CreateContext : Common.CommonContext
{
  public CreateContext(string dbNama) : base(dbNama) { }

  protected override void OnConfiguringHook(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
  }
}
