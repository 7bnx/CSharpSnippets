namespace CSharpSnippets.EFCore.Common.Entities
{
  public class PriceOffer
  {
    public int PriceOffersId { get; set; }
    public decimal NewPrice { get; set; }
    public string Text { get; set; } = null!;
    public int BookId { get; set; }
  }
}
