using CSharpSnippets.Configuration.IOptionsValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var hostBuilder = Host.CreateDefaultBuilder();
hostBuilder.ConfigureServices(services =>
{
  //services
  //  .AddOptionsWithValidateOnStart<SettingsWithIOptions, SettingsValidateOptions>("SettingsValidateOptions")
  //  .BindConfiguration("SettingsValidateOptions");

  //services
  //  .AddWithValidation<SettingsWithFluentValidation>("SettingsWithFluentValidation");

  services
    .AddWithValidation<SettingsWithFluentValidation, SettingsFluentValidator>("SettingsWithFluentValidation");

  //services.AddOptions<SettingsWithAnnotation>()
  //        .BindConfiguration("SettingsWithAnnotation")
  //        .ValidateDataAnnotations()
  //        .ValidateOnStart();

});

var host = hostBuilder.Build();

var z = host.Services.GetRequiredService<IOptions<SettingsWithFluentValidation>>();

host.Run();