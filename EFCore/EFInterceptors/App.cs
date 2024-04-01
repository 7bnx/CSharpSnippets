using CSharpSnippets.EFCore.EFInterceptors;
using Microsoft.EntityFrameworkCore;

DbContextOptionsBuilder<Context> builder = new();
builder.UseSqlite("Data source=EFInterceptors.db");
builder.AddInterceptors(new UpdateRemovableInterceptor());

using var context = new Context(builder.Options);

await context.Database.EnsureCreatedAsync();

var data = new Data();
await context.DataTable.AddAsync(data);
await context.SaveChangesAsync();

Print(data);

static void Print(Data data)
{
  Console.WriteLine("ID: " + data.ID);
  Console.WriteLine("Created at: " + data.CreatedAt);
  Console.WriteLine("Will remove at: " + data.WillRemoveAt);
}