﻿using CSharpSnippets.EFCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.EFCore.LoadingStrategies
{
  public class LoadingStrategiesContext : CommonContext
  {
    public LoadingStrategiesContext(string dbNama) : base(dbNama) { }
  }
}
