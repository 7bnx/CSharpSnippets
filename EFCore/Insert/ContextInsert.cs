using CSharpSnippets.EFCore.Common;
using CSharpSnippets.EFCore.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.EFCore.Insert
{
  public class ContextInsert : CommonContext
  {
    public ContextInsert(string dbName) : base(dbName)
    {
      base.EnsureDeleted();
      base.EnsureCreated();
    }
  }
}
