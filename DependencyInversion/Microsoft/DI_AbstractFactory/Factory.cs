namespace CSharpSnippets.DependencyInversion.Microsoft;

public interface IAbstractFactory<T>
{
  T Create();
}

public class AbstractFactory<T> : IAbstractFactory<T>
{
  private readonly Func<T> _factory;

  public AbstractFactory(Func<T> factory)
    => _factory = factory;
  public T Create()
    => _factory();
}

public class CustomFactory<T> : IAbstractFactory<T>
{
  private readonly Func<T> _factory;

  public CustomFactory(Func<T> factory)
    => _factory = factory;
  public T Create()
  {
    Console.WriteLine($"Instance by {nameof(CustomFactory<T>)}");
    return _factory();
  }
}
