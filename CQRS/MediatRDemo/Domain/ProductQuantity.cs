namespace CSharpSnippets.CQRS.MediatRDemo.Domain;
internal class ProductQuantity
{
  public int Value { get; init; }
  private ProductQuantity(int quantity)
  {
    Value = quantity;
  }

  public static ProductQuantity Create(int quantity)
  {
    return quantity <= 0
      ? throw new ArgumentOutOfRangeException(nameof(quantity))
      : new(quantity);
  }
}