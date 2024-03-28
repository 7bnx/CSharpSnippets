using MediatR;

namespace CSharpSnippets.CQRS.MediatRDemo.Queries;
internal class UpdateProductQuantityQuery : IRequest
{
  public string Name { get; init; }
  public int Quantity { get; init; }
}