using System.ComponentModel.DataAnnotations;

namespace CSharpSnippets.Configuration.IOptionsValidation;
internal class SettingsWithAnnotation
{
  [Required]
  public string StringField { get; init; } = null!;
  [Required]
  public int IntField { get; init; }
}