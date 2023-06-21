using CSharpSnippets.EFCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.EFCore.LoadingStrategies
{
  public class LoadingStrategiesContext : CommonContext
  {
    public LoadingStrategiesContext(string dbNama) : base(dbNama) { }

    protected override void OnConfiguringHook(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
    }
  }
}
