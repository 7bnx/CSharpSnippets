using CSharpSnippets.CQRS.MediatRDemo.Domain;
using CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
using MediatR;

namespace CSharpSnippets.CQRS.MediatRDemo.Commands;
internal class CreateProductCommandHandler(Repository repository) : IRequestHandler<CreateProductCommand>
{
  private readonly Repository _repository = repository;

  public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    if (await _repository.IsProductExistsAsync(request.Name, cancellationToken))
      return;

    var productName = ProductName.Create(request.Name);
    var productSKU = ProductSKU.Create(request.SKU);
    var productQuantity = ProductQuantity.Create(request.Quantity);

    var product = Product.Create(productName, productSKU, productQuantity);
    _ = await _repository.AddProductAsync(product, cancellationToken);
  }
}