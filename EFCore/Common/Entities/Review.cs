namespace CSharpSnippets.EFCore.Common.Entities
{
  public class Review
  {
    public int ReviewId { get; set; }
    public string Reviewer { get; set; } = null!;
    public decimal NumStars { get; set; }
    public string Text { get; set; } = null!;
    public int BookId { get; set; }
  }
}
