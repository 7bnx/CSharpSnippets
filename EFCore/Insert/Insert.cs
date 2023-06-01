using CSharpSnippets.EFCore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.EFCore.Insert
{
  public class Insert
  {
    public string DBName { get; set; }
    public Insert(string dBName)
       => DBName = dBName;

    public int DeleteDBAndInsert()
    {
      using Context context = new(DBName);
      Tag tag1 = new() { TagId = "Tag_1" };
      Tag tag2 = new() { TagId = "Tag_2" };
      Tag tag3 = new() { TagId = "Tag_3" };
      Author author1 = new() { Name = "Author_1" };
      Author author2 = new() { Name = "Author_2" };
      Author author3 = new() { Name = "Author_3" };
      BookAuthor bookAuthor1 = new() { Author = author1, Order = 0 };
      BookAuthor bookAuthor2 = new() { Author = author2, Order = 1 };
      BookAuthor bookAuthor3 = new() { Author = author3 };
      context.Books.Add(
        new Book()
        {
          Description = "Description_1",
          Price = 100,
          Title = "Book_1",
          Publisher = "Publisher_1",
          Url = "URL_1",
          PublishedOn = DateTime.Now,
          Tags = new Tag[] { tag1, tag2 },
          Reviews = new Review[] { new() { Text = "Review_1", NumStars = 3.8M, Reviewer = "Viewer_1" } },
          Promotion = new PriceOffer() { Text = "Promotion_1", NewPrice = 90 },
          AuthorsLink = new BookAuthor[] { bookAuthor1, bookAuthor2 }
        }
      );

      context.Books.Add(
        new Book()
        {
          Description = "Description_2",
          Price = 55,
          Title = "Book_2",
          Publisher = "Publisher_2",
          Url = "URL_2",
          PublishedOn = DateTime.Now,
          Tags = new Tag[] { tag3 },
          Reviews = new Review[] { new() { Text = "Review_2", NumStars = 5M, Reviewer = "Viewer_1" } },
          Promotion = new PriceOffer() { Text = "Promotion_2", NewPrice = 40 },
          AuthorsLink = new BookAuthor[] { new BookAuthor() { Author = author2 } }
        }
      );

      context.Books.Add(
        new Book()
        {
          Description = "Description_3",
          Price = 986,
          Title = "Book_3",
          Publisher = "Publisher_3",
          Url = "URL_3",
          PublishedOn = DateTime.Now,
          Tags = null!,
          Reviews = null!,
          Promotion = null!,
          AuthorsLink = new BookAuthor[] { bookAuthor3 }
        }
      );
      return context.SaveChanges();
    }
  }
}
