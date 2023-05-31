using System.ComponentModel.DataAnnotations;

namespace CSharpSnippets.EFCore.Common.Entities
{
  public class Tag
  {
    [Key]
    [Required]
    [MaxLength(40)]
    public string TagId { get; set; } = null!;

  }
}
