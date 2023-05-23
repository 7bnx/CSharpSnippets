using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnippets.Ado.Common;
using Microsoft.Data.Sqlite;

namespace CSharpSnippets.Ado.Read
{
  public class Reader
  {
    public string ConnectionString { get; init; }

    public Reader()
    {
      ConnectionString = Configuration.SqLiteConnectionString;
    }

    public object? GetAggregateValue(string tableName, string columnName, AggregateFunctions func) 
    {
      using SqliteConnection connection = new (ConnectionString);
      using SqliteCommand command = connection.CreateCommand();
      try
      {
        connection.Open();
        command.CommandText = $"SELECT {func}({columnName}) FROM {tableName}";
        return command.ExecuteScalar()!;
      }catch(SqliteException ex)
      {
        Console.WriteLine(ex.Message);
        return null;
      }
    }
    public List<List<object>> GetData(string tableName, params string[] columns) 
    {
      List<List<object>> list = new();
      using SqliteConnection connection = new (ConnectionString);
      using SqliteCommand command = connection.CreateCommand();
      try
      {
        connection.Open();
        command.CommandText = $"SELECT {string.Join(',', columns)} FROM {tableName}";
        using var reader = command.ExecuteReader();
        if(reader.HasRows)
        {
          while(reader.Read())
          {
            List<object> row = new();
            for (int i = 0; i < columns.Length; i++)
            {
              row.Add(reader[i]);
            }
            list.Add(row);
          }
        }
      }catch(SqliteException ex)
      {
        Console.WriteLine(ex.Message);
      }
      return list;
    }

  }
}
