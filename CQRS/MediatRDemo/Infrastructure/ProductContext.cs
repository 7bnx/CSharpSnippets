using Microsoft.EntityFrameworkCore;

namespace CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
internal class ProductContext : DbContext
{
  public DbSet<ProductModel> Products { get; set; }
  public ProductContext(DbContextOptions<ProductContext> options) : base(options)
  {

  }
}