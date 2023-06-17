using System.ComponentModel.DataAnnotations;

namespace CSharpSnippets.EFCore.Update;

public record ChangePubDateDTO
{
  public int BookId { get; set; }
  public string Title { get; set; }
  [DataType(DataType.DateTime)]
  public DateTime PublishedOn { get; set; }
}
