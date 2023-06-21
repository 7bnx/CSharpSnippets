namespace CSharpSnippets.DependencyInversion.Microsoft;

public interface IInjection
{
  public int Id { get; }
}

public class Injection : IInjection
{
  private static int _id;
  public int Id { get; init; }
  public Injection() => Id = ++_id;
}
