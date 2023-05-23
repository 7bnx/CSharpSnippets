using CSharpSnippets.Ado.Read;
using CSharpSnippets.Ado.Common;
using CSharpSnippets.Ado.Init;

Init init = new();

init.Drop();
init.Create();
init.Insert();

Reader reader = new();
Console.WriteLine("Aggregate values");
Console.Write("\t- Count of users: ");
Console.WriteLine(reader.GetAggregateValue("Users", "*", AggregateFunctions.COUNT));
Console.Write("\t- Max users age: ");
Console.WriteLine(reader.GetAggregateValue("Users", "age", AggregateFunctions.MAX));
Console.Write("\t- Min users age: ");
Console.WriteLine(reader.GetAggregateValue("Users", "age", AggregateFunctions.MIN));
Console.Write("\t- Agerage users age: ");
Console.WriteLine(reader.GetAggregateValue("Users", "age", AggregateFunctions.AVG));
Console.Write("\t- Sum of users age: ");
Console.WriteLine(reader.GetAggregateValue("Users", "age", AggregateFunctions.SUM));

Console.WriteLine();
Console.WriteLine("Select values");
var usersItems = reader.GetData("Users", "idUser", "firstName", "age");
List<User> users = new();
foreach (var item in usersItems)
{
  var user = new User(item);
  users.Add(user);
  Console.WriteLine($"\t- {user}");
}

var phonesItems = reader.GetData("Phones", "idPhone", "idUser", "Phone");
List<Phone> phones = new();
foreach (var item in phonesItems)
{
  var phone = new Phone(item);
  phones.Add(phone);
  Console.WriteLine($"\t- {phone}");
}