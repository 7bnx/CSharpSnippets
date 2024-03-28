using CSharpSnippets.CQRS.MediatRDemo.Commands;
using CSharpSnippets.CQRS.MediatRDemo.DataContracts;
using CSharpSnippets.CQRS.MediatRDemo.Queries;
using Microsoft.Extensions.Logging;

namespace CSharpSnippets.CQRS.MediatRDemo.Application;
internal class ApplicationService(MediatR.IMediator MediatR, ILogger<ApplicationService> Logger)
{

  public async Task CreateProductAsync(CreateProductDTO productToCreate)
  {
    var command = new CreateProductCommand()
    {
      Name = productToCreate.Name,
      SKU = productToCreate.SKU,
      Quantity = productToCreate.Quantity
    };
    await MediatR.Send(command);
  }

  public async Task<ProductDTO?> GetProductAsync(GetProductByNameDTO productName)
  {
    var command = new GetProductByNameQuery()
    {
      Name = productName.Name,
    };
    return await MediatR.Send(command);
  }

  public async Task UpdateProductQuantityAsync(UpdateProductQuantityDTO productQuantity)
  {
    var command = new UpdateProductQuantityQuery()
    {
      Name = productQuantity.Name,
      Quantity = productQuantity.Quantity
    };
    await MediatR.Send(command);
  }

  public async Task OperationAsync()
  {
    var productToCreate = new CreateProductDTO("Product1", 12345, 10);
    await CreateProductAsync(productToCreate);

    var productToFind = new GetProductByNameDTO(productToCreate.Name);
    var initialProduct = await GetProductAsync(productToFind)!;

    Logger.LogInformation("Product before update quantity {product}", initialProduct);

    var updatedQuantity = new UpdateProductQuantityDTO(initialProduct!.Name, initialProduct!.Quantity + 1);
    await UpdateProductQuantityAsync(updatedQuantity);

    var updatedProduct = await GetProductAsync(productToFind);

    Logger.LogInformation("Product after update quantity {product}", updatedProduct);
  }

}