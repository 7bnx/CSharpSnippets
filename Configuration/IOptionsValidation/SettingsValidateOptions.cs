using Microsoft.Extensions.Options;

namespace CSharpSnippets.Configuration.IOptionsValidation;

internal class SettingsValidateOptions : IValidateOptions<SettingsWithIOptions>
{
  public ValidateOptionsResult Validate(string? name, SettingsWithIOptions options)
  {
    if (string.IsNullOrEmpty(options.StringField))
      return ValidateOptionsResult.Fail("StrField is required");

    return ValidateOptionsResult.Success;
  }
}