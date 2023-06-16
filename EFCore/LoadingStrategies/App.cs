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

Console.WriteLine("Display entity with eager loading Promotion");
using (var context = new LoadingStrategiesContext(dbName))
{
  var book = context.Books
            .Include(book => book.Promotion)
            .First();
  Console.WriteLine(book);
}

Console.WriteLine("Display entity with eager loading and conditions");
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

Console.WriteLine("Explicit loading");
using (var context = new LoadingStrategiesContext(dbName))
{
  var book = context.Books.First();
  context.Entry(book).Collection(b => b.AuthorsLink).Load();
  foreach(var al in book.AuthorsLink)
  {
    context.Entry(al).Reference(a => a.Author).Load();
  }

  var numReviews = context.Entry(book).Collection(b => b.Reviews).Query().Count();
  var rating = context.Entry(book).Collection(b => b.Reviews).Query().Average(r => (double)r.NumStars);
  Console.WriteLine(book);
  Console.WriteLine($"Number of reviews: {numReviews}");
  Console.WriteLine($"Average rating: {rating}");
  Console.WriteLine();
}

Console.WriteLine("Select loading");
using(var context = new LoadingStrategiesContext(dbName))
{
  var books = context.Books.Select(b => new { b.Title, b.Description, b.Url }).ToList();
  foreach (var book in books)
    Console.WriteLine(book);
}