using MediatR;

namespace CSharpSnippets.CQRS.MediatRValidation;
internal class WithoutValidationHandler : IRequestHandler<WithoutValidationCommand>
{
  public Task Handle(WithoutValidationCommand request, CancellationToken cancellationToken)
  {
    Console.WriteLine("Command without validator");
    return Task.CompletedTask;
  }
}