using FluentValidation;

namespace CSharpSnippets.CQRS.MediatRValidation;
public sealed class CommandValidator : AbstractValidator<Command>
{
  public CommandValidator()
  {
    RuleFor(x => x.StrField)
      .NotEmpty()
      .WithMessage(x => $"{nameof(x.StrField)} can't be empty");

    RuleFor(x => x.IntField)
      .GreaterThan(0)
      .WithMessage(x => $"{nameof(x.IntField)} can't be less or equal zero");
  }
}