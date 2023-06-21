using CSharpSnippets.DependencyInversion.Microsoft;
using Microsoft.Extensions.DependencyInjection;


Console.WriteLine("Transient");
var serviceTransient = new ServiceCollection()
    .AddTransient<ILoggerService, ColoredConsoleLoggerService>()
    .AddTransient<Logger>();
var providerTransient = serviceTransient.BuildServiceProvider();
Output(providerTransient);
Console.WriteLine();

Console.WriteLine("Scoped");
var serviceScoped = new ServiceCollection()
    .AddScoped<ILoggerService, ColoredConsoleLoggerService>()
    .AddScoped<Logger>();
using var providerScoped = serviceScoped.BuildServiceProvider();
Console.WriteLine("First approach");
Output(providerScoped);
Console.WriteLine("Second approach");
Output(providerScoped);
Console.WriteLine();

Console.WriteLine("Singleton");
var serviceSingleton = new ServiceCollection()
    .AddSingleton<ILoggerService, ColoredConsoleLoggerService>()
    .AddSingleton<Logger>();
using var providerSingleton = serviceSingleton.BuildServiceProvider();
Console.WriteLine("First approach");
Output(providerSingleton);
Console.WriteLine("Second approach");
Output(providerSingleton);
Console.WriteLine();

static void Output(ServiceProvider provider)
{
  using var scope = provider.CreateScope();
  var loggerFirst = scope.ServiceProvider.GetService<Logger>()!;
  var loggerSecond = scope.ServiceProvider.GetService<Logger>()!;
  loggerFirst.Log("LoggerFirst");
  loggerSecond.Log("LoggerSecond");
}