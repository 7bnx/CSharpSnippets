using System.Text;

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
    public override string ToString()
    {
      StringBuilder sb = new();
      sb.Append($"Book '{Title}'{Environment.NewLine}");
      sb.Append($"\tId: {BookId}{Environment.NewLine}");
      sb.Append($"\tDescription: {Description}{Environment.NewLine}");
      sb.Append($"\tPublished on {PublishedOn} by {Publisher}{Environment.NewLine}");
      sb.Append($"\tRegular price: {Price}{Environment.NewLine}");
      sb.Append($"\tUrl: {Url}{Environment.NewLine}");
      
      sb.Append($"\tPromotion{Environment.NewLine}");
      if (Promotion is not null)
      {
        sb.Append($"\t\tOffer: {Promotion.Text}{Environment.NewLine}");
        sb.Append($"\t\tNew price: {Promotion.NewPrice}{Environment.NewLine}");
      } else
        sb.Append($"\t\tPromotion is null or empty{Environment.NewLine}");
      
      sb.Append($"\tTags{Environment.NewLine}");
      if (Tags is not null && Tags.Count > 0)
      {
        int i = 1;
        foreach (var tag in Tags)
          sb.Append($"\t\t{i++}\t{tag.TagId}{Environment.NewLine}");
      } else
        sb.Append($"\t\tTags is null or empty{Environment.NewLine}");

      sb.Append($"\tAuthors{Environment.NewLine}");
      if (AuthorsLink is not null && AuthorsLink.Count > 0)
      {
        int i = 1;
        foreach (var author in AuthorsLink)
          sb.Append($"\t\t{i++}\t{author.Author?.Name}{Environment.NewLine}");
      }else
        sb.Append($"\t\tAuthors is null or empty{Environment.NewLine}");
      return sb.ToString();
    }

  }
}
