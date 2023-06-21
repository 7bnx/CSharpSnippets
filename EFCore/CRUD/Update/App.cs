using CSharpSnippets.EFCore.Common.Entities;
using CSharpSnippets.EFCore.Insert;
using CSharpSnippets.EFCore.Update;

const string dbName = "Update";

Insert insert = new(dbName);
insert.DeleteDBAndInsert();

Book book;
ChangePubDateDTO bookDTO;

Console.WriteLine("Update in single context");
using (var context = new UpdateContext(dbName))
{
  book = context.Books.Where(b => b.Description == "Description_1").First();
  Console.WriteLine($"State found: {context.Entry(book).State}");
  book.PublishedOn = new DateTime(1111, 11, 11);
  Console.WriteLine($"State after change: {context.Entry(book).State}");
  var updateCount = context.SaveChanges();
  Console.WriteLine($"State save changes: {context.Entry(book).State}");
  Console.WriteLine($"Entities updated: {updateCount}");
  Console.WriteLine();
}

Console.WriteLine("Update in different context");
using (var context = new UpdateContext(dbName))
{
  var service = new ChangePubDateService(context);
  bookDTO = service.GetOriginal(1);
  Console.WriteLine(bookDTO);
}

using (var context = new UpdateContext(dbName))
{
  var service = new ChangePubDateService(context);
  bookDTO = bookDTO with { PublishedOn = new DateTime(01, 01, 01) };
  service.Update(bookDTO);
}

using (var context = new UpdateContext(dbName))
{
  Console.WriteLine(context.Books.First());
}