using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnippets.Ado.Common;
using Microsoft.Data.Sqlite;

namespace CSharpSnippets.Ado.Init
{
  public class Init
  {
    public string ConnectionString { get; init; }
    public Init()
    {
      ConnectionString = Configuration.SqLiteConnectionString;
    }
    public void Drop() 
    {
      Process(table => $"DROP TABLE IF EXISTS {table.Name}");
    }

    public void Create()
    {
      Process(table => $"CREATE TABLE IF NOT EXISTS {table.Name} ({table.Shema})");
    }

    public void Insert()
    {
      Process(table => $"INSERT INTO {table.Name} VALUES {table.Insertion}");
    }
    private void Process(Func<TableConfiguration, string> func)
    {
      using SqliteConnection connection = new(ConnectionString);
      using var command = connection.CreateCommand();
      try
      {
        connection.Open();
        foreach (var table in Configuration.TableConfiguration)
        {
          command.CommandText = func(table);
          command.ExecuteNonQuery();
        }
      } catch (SqliteException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
