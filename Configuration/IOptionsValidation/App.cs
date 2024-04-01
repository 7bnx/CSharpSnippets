using CSharpSnippets.Configuration.IOptionsValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder();
hostBuilder.ConfigureServices(services =>
{
  services
    .AddOptionsWithValidateOnStart<SettingsWithIOptions, SettingsValidateOptions>("SettingsValidateOptions")
    .BindConfiguration("SettingsValidateOptions");

  services
    .AddWithValidation<SettingsWithFluentValidation, SettingsFluentValidator>("SettingsWithFluentValidation");

  services.AddOptions<SettingsWithAnnotation>()
          .BindConfiguration("SettingsWithAnnotation")
          .ValidateDataAnnotations()
          .ValidateOnStart();

});
var host = hostBuilder.Build();

host.Run();