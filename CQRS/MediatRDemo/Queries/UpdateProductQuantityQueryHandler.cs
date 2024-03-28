using CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
using MediatR;

namespace CSharpSnippets.CQRS.MediatRDemo.Queries;
internal class UpdateProductQuantityQueryHandler(Repository repository) : IRequestHandler<UpdateProductQuantityQuery>
{
  private readonly Repository _repository = repository;

  public async Task Handle(UpdateProductQuantityQuery request, CancellationToken cancellationToken)
  {
    await _repository.UpdateQuantity(request.Name, request.Quantity, cancellationToken);
  }
}