using CSharpSnippets.CQRS.MediatRDemo.DataContracts;
using CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
using MediatR;

namespace CSharpSnippets.CQRS.MediatRDemo.Queries;
internal class GetProductByNameQueryHandler(Repository Repository)
  : IRequestHandler<GetProductByNameQuery, ProductDTO?>
{
  public async Task<ProductDTO?> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
  {
    return await Repository.GetByNameAsync(request.Name, cancellationToken);
  }
}