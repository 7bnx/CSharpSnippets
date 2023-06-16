using CSharpSnippets.EFCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CSharpSnippets.EFCore.ClientServerEvaluation;

public class ClientServerEvaluationContext : CommonContext
{
  public ClientServerEvaluationContext(string dbNama) : base(dbNama) { }

  protected override void OnConfiguringHook(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
  }
}
