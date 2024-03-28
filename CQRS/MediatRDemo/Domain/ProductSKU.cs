namespace CSharpSnippets.CQRS.MediatRDemo.Domain;
internal class ProductSKU
{
  public int Value { get; init; }
  private ProductSKU(int sku)
  {
    Value = sku;
  }

  public static ProductSKU Create(int sku)
  {
    return sku <= 0
      ? throw new ArgumentOutOfRangeException(nameof(sku))
      : new(sku);
  }
}