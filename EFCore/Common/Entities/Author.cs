namespace CSharpSnippets.EFCore.Common.Entities
{
  public class Author
  {
    public int AuthorId { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BookAuthor> BooksList { get; set; } = null!;
  }
}
