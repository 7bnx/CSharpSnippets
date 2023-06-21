using CSharpSnippets.EFCore.ClientServerEvaluation;
using CSharpSnippets.EFCore.Insert;

const string dbName = "ClientServerEvaluation";

Insert insert = new(dbName);
insert.DeleteDBAndInsert();

using (var context = new ClientServerEvaluationContext(dbName))
{
  var book = context.Books.Select(b => new 
    {
      b.BookId, 
      b.Title, 
      Authors = string.Join(',', 
                  b.AuthorsLink
                  .OrderBy(a => a.Order)
                  .Select(a => a.Author.Name))
    }).First();
  Console.WriteLine(book);
}