using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnippets.Ado.Common;
using Microsoft.Data.Sqlite;

namespace CSharpSnippets.Ado.Parameters
{
  public class Parameters
  {
    public string ConnectionString { get; init; }
    private static Dictionary<WhereComparison, string> _whereComparison = new()
    { 
      {WhereComparison.EUQAL, "=" },
      {WhereComparison.NOT_EUQAL, "<>" },
      {WhereComparison.GREATER_OR_EQALS, ">=" },
      {WhereComparison.GREATER, ">" },
      {WhereComparison.LESS_OR_EQALS, "<=" },
      {WhereComparison.LESS, "<" }
    };
    public Parameters()
    {
      ConnectionString = Configuration.SqLiteConnectionString;
    }

    private List<List<object>> Filter(
      bool withInjection,
      string tableName, 
      string filterColumn, 
      object value,
      WhereComparison comparison,
      params string[] columns)
    {
      List<List<object>> list = new();
      using SqliteConnection connection = new(ConnectionString);
      using SqliteCommand command = connection.CreateCommand();
      try
      {
        connection.Open();
        if (!withInjection)
        {
          command.CommandText = $"SELECT {string.Join(',', columns)} FROM {tableName} WHERE {filterColumn} {_whereComparison[comparison]} @value";
          command.Parameters.AddWithValue("@value", value);
        }else
          command.CommandText = $"SELECT {string.Join(',', columns)} FROM {tableName} WHERE {filterColumn} {_whereComparison[comparison]} {value}";
        using var reader = command.ExecuteReader();
        if(reader.HasRows)
        {
          while(reader.Read())
          {
            List<object> row = new();
            for (int i = 0; i < columns.Length; i++)
              row.Add(reader[i]);
            list.Add(row);
          }
        }
      }catch (SqliteException ex)
      {

      }
      return list;
    }
    public List<List<object>> FilterWithSQLInjection(
      string tableName,
      string filterColumn,
      object value,
      WhereComparison comparison,
      params string[] columns)
    {
      return Filter(true, tableName, filterColumn, value, comparison, columns);
    }
    public List<List<object>> FilterWithParameters(
      string tableName,
      string filterColumn,
      object value,
      WhereComparison comparison,
      params string[] columns)
    {
      return Filter(false, tableName, filterColumn, value, comparison, columns);
    }
  }
  public enum WhereComparison
  {
    EUQAL,
    NOT_EUQAL,
    GREATER_OR_EQALS,
    GREATER,
    LESS_OR_EQALS,
    LESS
  }
}
