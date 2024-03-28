using MediatR;

namespace CSharpSnippets.CQRS.MediatRDemo.Commands;
internal class CreateProductCommand : IRequest
{
  public string Name { get; init; } = null!;
  public int SKU { get; init; }
  public int Quantity { get; init; }
}