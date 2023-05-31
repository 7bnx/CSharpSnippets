namespace CSharpSnippets.EFCore.Common.Entities
{
  public class Book
  {
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime PublishedOn { get; set; }
    public string Publisher { get; set; } = null!;
    public decimal Price { get; set; }
    public string Url { get; set; } = null!;

    public PriceOffer Promotion { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;

    public ICollection<Tag> Tags { get; set; } = null!;

    public ICollection<BookAuthor> AuthorsLink { get; set; } = null!;

  }
}
