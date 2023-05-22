using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.Ado.Common
{
  public readonly record struct User(long idUser, string firstName, int age)
  {
    public User(IEnumerable<object> data) :
      this((long)data.ElementAt(0), (string)data.ElementAt(1), (int)(long)data.ElementAt(2))
    { }
  }

  public readonly record struct Phone(long idPhone, long idUser, string phone)
  {
    public Phone(IEnumerable<object> data) :
      this((long)data.ElementAt(0), (long)data.ElementAt(1), (string)data.ElementAt(2))
    { }
  }
}
