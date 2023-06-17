using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnippets.EFCore.Common;

namespace CSharpSnippets.EFCore.Update;

public class UpdateContext : CommonContext
{
  public UpdateContext(string dbNama) : base(dbNama) { }

  protected override void OnConfiguringHook(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
  }
}
