using FluentValidation;
using MediatR;

namespace CSharpSnippets.CQRS.MediatRValidation;
internal class ValidationBehavior<TRequest, TResponse>
  : IPipelineBehavior<TRequest, TResponse>
   where TRequest : notnull
{

  private readonly IEnumerable<IValidator<TRequest>>? _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle
  (
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    var context = new ValidationContext<TRequest>(request);
    var validationFailures = await Task.WhenAll(
    _validators!.Select(async validator => await validator.ValidateAsync(context)));

    var errors = validationFailures
        .Where(validationResult => !validationResult.IsValid)
        .SelectMany(validationResult => validationResult.Errors)
        .Select(validationFailure => new
        {
          validationFailure.PropertyName,
          validationFailure.ErrorMessage
        })
        .ToList();

    if (errors.Count != 0)
    {
      Console.WriteLine("Error");
      return default!;
    }

    var response = await next();

    return response;
  }
}