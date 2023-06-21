using CSharpSnippets.DependencyInversion.Microsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddTransient<IConfiguration>(_ =>
      new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", true) // release file
      .AddJsonFile($"appsettings.Development.json", true) // debug file
      .Build()
    )
    .AddTransient( // Add class with required parameters
      provider =>
      (IParameters)provider
      .GetRequiredService<IConfiguration>()
      .GetSection("Parameters").Get<Parameters>()!
    )
    .AddTransient<ClassWithParameters>()
    .AddTransient( // choose implementation
      provider => 
        (IAppsettingDependentClass)Activator
        .CreateInstance(Type.GetType(provider.GetRequiredService<IConfiguration>()["DependentClass"]!)!)!
     );

var builder = services.BuildServiceProvider();
var classWithParameters = builder.GetRequiredService<ClassWithParameters>();
var dependentClass = builder.GetRequiredService<IAppsettingDependentClass>();
classWithParameters.Write();
dependentClass.Write();
Console.ReadLine();