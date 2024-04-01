using MediatR;

namespace CSharpSnippets.CQRS.MediatRValidation;
internal class LoggingBehavior<TRequest, TResult>
  : IPipelineBehavior<TRequest, TResult>
  where TRequest : notnull
{
  public async Task<TResult> Handle
  (
    TRequest request,
    RequestHandlerDelegate<TResult> next,
    CancellationToken cancellationToken
  )
  {
    Console.WriteLine("Start");

    var result = await next();

    Console.WriteLine("End");

    return result;
  }
}