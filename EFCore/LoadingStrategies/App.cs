using CSharpSnippets.EFCore.Insert;
using CSharpSnippets.EFCore.LoadingStrategies;
using Microsoft.EntityFrameworkCore;

var dbName = "loadingStrategies";

Insert insert = new(dbName);
insert.DeleteDBAndInsert();

Console.WriteLine("Display entity without relation data");
using (var context = new LoadingStrategiesContext(dbName))
{
  var book = context.Books.First();
  Console.WriteLine(book);
}

Console.WriteLine("Display entity with eager loading");
using (var context = new LoadingStrategiesContext(dbName))
{
  var book = context.Books
            .Include(book => book.AuthorsLink.Where(a => a.Author.Name == "Author_1"))
            .ThenInclude(authorBook => authorBook.Author)
            .Include(book => book.Promotion)
            .Include(book => book.Tags)
            .First();
  Console.WriteLine(book);
}