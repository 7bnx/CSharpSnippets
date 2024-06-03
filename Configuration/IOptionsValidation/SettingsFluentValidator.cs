using FluentValidation;

namespace CSharpSnippets.Configuration.IOptionsValidation;
internal class SettingsFluentValidator : AbstractValidator<SettingsWithFluentValidation>
{
  public SettingsFluentValidator()
  {
    RuleFor(x => x.StringField).NotEmpty().NotNull();
    RuleFor(x => x.List).NotEmpty().WithMessage("Lol Kek");
  }
}