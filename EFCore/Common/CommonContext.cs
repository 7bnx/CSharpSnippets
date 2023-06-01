using CSharpSnippets.EFCore.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.EFCore.Common
{
  public class CommonContext : DbContext
  {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<PriceOffer> PriceOffers { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public bool EnsureDeleted() => Database.EnsureDeleted();
    public bool EnsureCreated() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=entity.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId });
    }
  }
}
