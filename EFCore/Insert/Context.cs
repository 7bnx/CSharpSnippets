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
  public class Context : CommonContext
  {
    public Context()
    {
      base.EnsureDeleted();
      base.EnsureCreated();
    }
  }
}
