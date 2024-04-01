using Microsoft.EntityFrameworkCore;

namespace CSharpSnippets.EFCore.EFInterceptors;
internal class Context : DbContext
{
  public DbSet<Data> DataTable { get; set; }

  public Context(DbContextOptions<Context> options) : base(options)
  {

  }
}