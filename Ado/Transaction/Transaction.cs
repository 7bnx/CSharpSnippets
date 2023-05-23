using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnippets.Ado.Common;
using Microsoft.Data.Sqlite;

namespace CSharpSnippets.Ado.Transaction
{
  public class Transaction
  {
    public string ConnectionString { get; init; }
    public Transaction()
    {
      ConnectionString = Configuration.SqLiteConnectionString;
    }

    public List<List<object>> Start(bool interruptTransaction, string tableName, string[] columns, object[] values)
    {
      using SqliteConnection connection = new(ConnectionString);
      using SqliteCommand command = connection.CreateCommand();
      SqliteTransaction transaction = null!;
      try
      {
        connection.Open();
        transaction = connection.BeginTransaction(); //after connections was opened
        command.Transaction = transaction;
        command.CommandText = $"INSERT INTO {tableName} ({string.Join(',', columns)}) VALUES({string.Join(',', values)})";
        command.ExecuteNonQuery();
        if (interruptTransaction) 
          throw new Exception("Transaction was interrupted");
        transaction.Commit();
      } catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        transaction?.Rollback();
      }
      return Read(command, tableName, columns);
    }

    private static List<List<object>> Read(SqliteCommand command, string tableName, string[] columns)
    {
      List<List<object>> list = new();
      try
      {
        command.CommandText = $"SELECT {string.Join(',', columns)} FROM {tableName}";
        command.Transaction = null;
        using var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
          while (reader.Read())
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
