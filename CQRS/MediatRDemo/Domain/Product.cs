namespace CSharpSnippets.CQRS.MediatRDemo.Domain;
internal class Product
{
  public Guid Id { get; }
  public string Name => _productName.Value;
  public int SKU => _productSKU.Value;
  public int Quantity => _productQuantity.Value;
  private readonly ProductName _productName;
  private readonly ProductSKU _productSKU;
  private readonly ProductQuantity _productQuantity;
  private Product(ProductName name, ProductSKU sku, ProductQuantity quantity)
  {
    Id = Guid.NewGuid();
    _productName = name;
    _productSKU = sku;
    _productQuantity = quantity;
  }
  public static Product Create(ProductName name, ProductSKU sku, ProductQuantity quantity)
  {
    return new(name, sku, quantity);
  }
}