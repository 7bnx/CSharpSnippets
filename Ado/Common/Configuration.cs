using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public static class Configuration
  {
    private const string SettingsFileName = "appsettings.json";
    static Configuration()
    {
      var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(SettingsFileName, true, true);
      var config = builder.Build();
    }

  }
}