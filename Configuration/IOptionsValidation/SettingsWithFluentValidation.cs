namespace CSharpSnippets.Configuration.IOptionsValidation;

internal class SettingsWithFluentValidation
{
  public string StringField { get; init; } = null!;
  public int IntField { get; init; }
  public IReadOnlyList<int> List { get; init; } = Array.Empty<int>();
}