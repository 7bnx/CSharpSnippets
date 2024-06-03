using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CSharpSnippets.Configuration.IOptionsValidation;
internal static class Extensions
{
  public static OptionsBuilder<TOptions> AddWithValidation<TOptions, TValidator>(
    this IServiceCollection services,
    string configurationSection)
    where TOptions : class
    where TValidator : class, IValidator<TOptions>
  {
    //Add validator
    services.AddScoped<IValidator<TOptions>, TValidator>();

    return services.AddOptions<TOptions>()
        .BindConfiguration(configurationSection)
        .ValidateFluentValidation()
        .ValidateOnStart();
  }

  public static OptionsBuilder<TOptions> AddWithValidation<TOptions>
  (
    this IServiceCollection services,
    string configurationSection
  ) where TOptions : class
  {
    var validator = AssemblyScanner
      .FindValidatorsInAssembly(Assembly.GetExecutingAssembly(), true)
      .FirstOrDefault(validator => typeof(IValidator<TOptions>).IsAssignableFrom(validator.ValidatorType))?
      .ValidatorType;

    var optionsBuilder = services
        .AddOptions<TOptions>()
        .BindConfiguration(configurationSection);

    if (validator is null)
      return optionsBuilder;

    services.AddScoped(typeof(IValidator<TOptions>), validator);

    return optionsBuilder
      .ValidateFluentValidation()
      .ValidateOnStart();
  }

  public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>
  (
    this OptionsBuilder<TOptions> optionsBuilder
  ) where TOptions : class
  {
    optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
        provider => new FluentValidationOptions<TOptions>(
          optionsBuilder.Name, provider));
    return optionsBuilder;
  }
}