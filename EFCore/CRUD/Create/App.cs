using CSharpSnippets.EFCore.Common.Entities;
using CSharpSnippets.EFCore.Create;
using CSharpSnippets.EFCore.Insert;

const string dbName = "Create";

Insert insert = new(dbName);
insert.DeleteDBAndInsert();

using(var context = new CreateContext(dbName))
{
  var book = new Book()
  {
    Title = "Book To Create",
    Description = "Description",
    PublishedOn = DateTime.Today,
    Publisher = "Publisher",
    Url = "URL",
    Reviews = new List<Review>()
    {
      new Review()
      {
        NumStars = 3,
        Text = "Comment Text 1",
        Reviewer = "Name1"
      },
      new Review()
      {
        NumStars = 3,
        Text = "Comment Text 2",
        Reviewer = "Name2"
      }
    }
  };
  Console.WriteLine($"State before add: {context.Entry(book).State}");
  var tt = context.Books.Add(book);
  Console.WriteLine($"State after add: {context.Entry(book).State}");
  var createdCount = context.SaveChanges();
  Console.WriteLine($"State save changes: {context.Entry(book).State}");
  Console.WriteLine($"Entities created: {createdCount}");
}
