using Microsoft.Extensions.DependencyInjection;
using CSharpSnippets.DependencyInversion.Microsoft;

Console.WriteLine($"Some templates of using DI with factory pattern{Environment.NewLine}");

Console.WriteLine("Default-implemented factory with concrete-injection type");

var serviceConcrete = new ServiceCollection()
                          .AddFactory<Injection>() // injection
                          .AddSingleton<ClassWithConcreteDependency>(); // dependent type
var providerConcrete = serviceConcrete.BuildServiceProvider();
var ConcreteFactory = providerConcrete.GetService<ClassWithConcreteDependency>()!;

Output(ConcreteFactory);

Console.WriteLine(Environment.NewLine);

Console.WriteLine("Default-implemented factory with interface-injection type");

var serviceAbstract = new ServiceCollection()
                  .AddFactory<IInjection, Injection>() // injection
                  .AddSingleton<ClassWithAbstractDependency>(); //dependent type
var providerAbstract = serviceAbstract.BuildServiceProvider();
var abstractDependency = providerAbstract.GetService<ClassWithAbstractDependency>()!;

Output(abstractDependency);

Console.WriteLine(Environment.NewLine);

Console.WriteLine("Custom-implemented factory with interface-injection type");

var serviceCustomFactory = new ServiceCollection()
                          .AddFactory<IInjection, Injection, CustomFactory<IInjection>>() // injection
                          .AddSingleton<ClassWithAbstractDependency>(); // dependent type
var providerAbstractCustomFactory = serviceCustomFactory.BuildServiceProvider();
var abstractCustomFactory = providerAbstractCustomFactory.GetService<ClassWithAbstractDependency>()!;

Output(abstractCustomFactory);

static void Output(IDependentClass instance)
{
  for (int i = 0; i < 3; i++)
    instance.Show();
}
