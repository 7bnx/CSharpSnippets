using Microsoft.Extensions.DependencyInjection;

namespace CSharpSnippets.DependencyInversion.Microsoft;

public static class ServiceExtension
{
  public static IServiceCollection AddFactory<T>(this IServiceCollection services) where T : class
  {
    return services
      .AddTransient<T>()
      .AddSingleton<Func<T>>(x => () => x.GetService<T>()!)
      .AddSingleton<IAbstractFactory<T>, AbstractFactory<T>>();
  }

  public static IServiceCollection AddFactory<T, U, Z>(this IServiceCollection services) where T : class where U : class, T where Z : class, IAbstractFactory<T>
  {
    return services
      .AddTransient<T, U>()
      .AddSingleton<Func<T>>(x => () => x.GetService<T>()!)
      .AddSingleton<IAbstractFactory<T>, Z>();
  }

  public static IServiceCollection AddFactory<T, U>(this IServiceCollection services) where T : class where U : class, T
  {
    return services
      .AddTransient<T, U>()
      .AddSingleton<Func<T>>(x => () => x.GetService<T>()!)
      .AddSingleton<IAbstractFactory<T>, AbstractFactory<T>>();
  }
}
