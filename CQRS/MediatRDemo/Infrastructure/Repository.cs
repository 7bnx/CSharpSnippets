using CSharpSnippets.CQRS.MediatRDemo.DataContracts;
using CSharpSnippets.CQRS.MediatRDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
internal class Repository(IDbContextFactory<ProductContext> DbContextFactory)
{
  public async Task<Guid> AddProductAsync(Product productToAdd, CancellationToken token = default)
  {
    await using var context = await DbContextFactory.CreateDbContextAsync(token);
    var product = new ProductModel()
    {
      Id = productToAdd.Id,
      Name = productToAdd.Name,
      Quantity = productToAdd.Quantity,
      SKU = productToAdd.SKU
    };
    await context.Products.AddAsync(product, token);
    await context.SaveChangesAsync(token);
    return productToAdd.Id;
  }

  public async Task<bool> IsProductExistsAsync(Guid id, CancellationToken token = default)
  {
    await using var context = await DbContextFactory.CreateDbContextAsync(token);
    return await context.Products.AnyAsync(p => p.Id == id, token);
  }

  public async Task<bool> IsProductExistsAsync(string name, CancellationToken token = default)
  {
    await using var context = await DbContextFactory.CreateDbContextAsync(token);
    return await context.Products.AnyAsync(p => p.Name == name, token);
  }

  public async Task<ProductDTO?> GetByNameAsync(string name, CancellationToken token = default)
  {
    await using var context = await DbContextFactory.CreateDbContextAsync(token);
    return await context.Products.AsNoTracking()
      .Where(p => p.Name == name)
      .Select(p => new ProductDTO(p.Name, p.SKU, p.Quantity))
      .FirstOrDefaultAsync(cancellationToken: token);
  }

  public async Task UpdateQuantity(string name, int quantity, CancellationToken token = default)
  {
    await using var context = await DbContextFactory.CreateDbContextAsync(token);
    await context.Products.AsNoTracking()
      .Where(p => p.Name == name)
      .ExecuteUpdateAsync(s => s.SetProperty(p => p.Quantity, p => quantity));
  }

}