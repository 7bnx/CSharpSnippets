using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.RabbitMQ.Common
{
  static public class Configuration
  {
    public static ConnectionFactoryConfiguration ConnectionFactory { get; }
    private const string SettingsFileName = "appsettings.json";
    static Configuration()
    {
      var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(SettingsFileName, true, true);
      var config = builder.Build();
      var connectionFactorySection = config.GetSection("connectionFactory");

      ConnectionFactory = GetConnectionFactoryConfiguration(connectionFactorySection);
    }

    private static ConnectionFactoryConfiguration GetConnectionFactoryConfiguration(IConfigurationSection connectionFactorySection)
    {
      var ConnectionFactory = new ConnectionFactoryConfiguration
      (
        HostName: connectionFactorySection["hostName"]!,
        UserName: connectionFactorySection["userName"]!,
        Password: connectionFactorySection["password"]!
      );
      if(int.TryParse(connectionFactorySection["port"], out var port))
        return ConnectionFactory with { Port = port };
      return ConnectionFactory;
    }

    public readonly record struct ConnectionFactoryConfiguration
    (
      string HostName = "localhost",
      int Port = 5672,
      string UserName = "guest",
      string Password = "guest"
    )
    {
      public string HostName { get; init; } = HostName ?? "localhost";
      public string UserName { get; init; } = UserName ?? "guest";
      public string Password { get; init; } = Password ?? "guest";
    }
  }
}
