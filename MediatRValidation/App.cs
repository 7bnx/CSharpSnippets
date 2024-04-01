using System.Reflection;
using CSharpSnippets.CQRS.MediatRValidation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var service = new ServiceCollection();

service.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
  config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
service.AddSingleton<App>();

var app = service.BuildServiceProvider().GetService<App>();

app!.Execute(new(Guid.NewGuid(), null, 1));
app!.ExecuteWithoutValidation(new(Guid.NewGuid(), null, 1));


class App(IMediator mediator)
{
  public async void Execute(Command command)
  {
    await mediator.Send(command);
  }

  public async void ExecuteWithoutValidation(WithoutValidationCommand command)
  {
    await mediator.Send(command);
  }
}