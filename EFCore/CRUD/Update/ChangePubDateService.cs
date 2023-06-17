using CSharpSnippets.EFCore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpSnippets.EFCore.Update;

public class ChangePubDateService
{
  private readonly UpdateContext _context;
  public ChangePubDateService(UpdateContext context)
  {
    _context = context;
  }

  public ChangePubDateDTO GetOriginal(int id)
  {
    return _context.Books
          .Select(p => new ChangePubDateDTO 
            {
              BookId = p.BookId, 
              Title = p.Title, 
              PublishedOn = p.PublishedOn 
          })
          .Single(b => b.BookId == id);
  }

  public Book Update(ChangePubDateDTO dto)
  {
    var book = _context.Books.Single(b => b.BookId == dto.BookId);
    book.PublishedOn = dto.PublishedOn;
    _context.SaveChanges();
    return book;
  }
}
