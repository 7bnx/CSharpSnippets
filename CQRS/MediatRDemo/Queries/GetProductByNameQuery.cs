using CSharpSnippets.CQRS.MediatRDemo.DataContracts;
using MediatR;

namespace CSharpSnippets.CQRS.MediatRDemo.Queries;
internal class GetProductByNameQuery : IRequest<ProductDTO?>
{
  public string Name { get; init; } = null!;

}