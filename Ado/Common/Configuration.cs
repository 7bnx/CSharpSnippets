using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Collections.Immutable;

namespace CSharpSnippets.Ado.Common
{
  public static class Configuration
  {
    private const string SettingsFileName = "appsettings.json";
    private const string SqLiteConnectionStringName = "SqLite";
    private const string TablesConfigurationSectionName = "TablesConfiguration";
    public static ImmutableList<TableConfiguration> TableConfiguration { get; }
    public static string SqLiteConnectionString { get; }
    static Configuration()
    {
      var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(SettingsFileName, true, true);
      var config = builder.Build();
      SqLiteConnectionString = config.GetConnectionString(SqLiteConnectionStringName)!;
      
      TableConfiguration = ImmutableList.CreateRange(GetTabledConfiguration(config));
    }
    static IEnumerable<TableConfiguration> GetTabledConfiguration(IConfigurationRoot config)
    {
      List<TableConfiguration> list = new();
      var section = config.GetSection(TablesConfigurationSectionName);
      foreach(var ch in section.GetChildren())
        list.Add(new TableConfiguration(ch["Name"]!, ch["Shema"]!, ch["Insertion"]!));
      return list;
    }

  }

  public readonly record struct TableConfiguration(string Name, string Shema, string Insertion);
}