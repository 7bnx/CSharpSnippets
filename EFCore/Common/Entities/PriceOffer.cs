namespace CSharpSnippets.EFCore.Common.Entities
{
  public class PriceOffer
  {
    public int PriceOfferId { get; set; }
    public decimal NewPrice { get; set; }
    public string Text { get; set; } = null!;
    public int BookId { get; set; }
  }
}
