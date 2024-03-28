namespace CSharpSnippets.CQRS.MediatRDemo.Domain;
internal class ProductName
{
  public string Value { get; init; }
  private ProductName(string name)
  {
    Value = name;
  }
  public static ProductName Create(string name)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof(name));
    return new ProductName(name);
  }
}