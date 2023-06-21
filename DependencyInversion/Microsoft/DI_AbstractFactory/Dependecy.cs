namespace CSharpSnippets.DependencyInversion.Microsoft;

public interface IDependentClass
{
  void Show();
}

public class ClassWithAbstractDependency : IDependentClass
{
  private readonly IAbstractFactory<IInjection> _factory;
  public ClassWithAbstractDependency(IAbstractFactory<IInjection> factory)
    => _factory = factory;
  public void Show()
    => Console.WriteLine(_factory.Create().Id);
}

public class ClassWithConcreteDependency : IDependentClass
{
  private readonly IAbstractFactory<Injection> _factory;
  public ClassWithConcreteDependency(IAbstractFactory<Injection> factory)
    => _factory = factory;
  public void Show()
    => Console.WriteLine(_factory.Create().Id);
}
