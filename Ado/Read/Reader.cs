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



  }
}
