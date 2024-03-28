namespace CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
internal class ProductModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public int SKU { get; set; }
  public int Quantity { get; set; }
}