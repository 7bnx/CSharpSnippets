using MediatR;

namespace CSharpSnippets.CQRS.MediatRValidation;
internal class CommandHandler : IRequestHandler<Command, int>
{

  public Task<int> Handle(Command request, CancellationToken cancellationToken)
  {
    Console.WriteLine("Command with Validator");
    return Task.FromResult(123);
  }
}