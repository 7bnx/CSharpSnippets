using CSharpSnippets.Ado.Common;
using CSharpSnippets.Ado.Init;
using CSharpSnippets.Ado.Parameters;

Init init = new();
init.Drop();
init.Create();
init.Insert();


Parameters param = new();

Console.WriteLine("Legal query with sql parameters");
var queryResult = param.FilterWithParameters(
  "Users",
  "age",
  "25",
  WhereComparison.GREATER,
  "idUser", "firstName", "age");
Console.WriteLine($"Rows count: {queryResult.Count}");
foreach (var k in queryResult)
  Console.WriteLine($"\t- {new User(k)}");

Console.WriteLine();
Console.WriteLine("Illegal query with sql injection");
queryResult = param.FilterWithSQLInjection(
  "Users", 
  "age", 
  "25 UNION SELECT idUser, Phone, idPhone FROM Phones", 
  WhereComparison.GREATER, 
  "idUser", "firstName", "age");
Console.WriteLine($"Rows count: {queryResult.Count}");
foreach (var k in queryResult)
  Console.WriteLine($"\t- {new User(k)}");

Console.WriteLine();
Console.WriteLine("Illegal query with sql parameters");
queryResult = param.FilterWithParameters(
  "Users",
  "age",
  "25 UNION SELECT idUser, Phone, idPhone FROM Phones",
  WhereComparison.GREATER,
  "idUser", "firstName", "age");
Console.WriteLine($"Rows count: {queryResult.Count}");
foreach (var k in queryResult)
  Console.WriteLine($"\t- {new User(k)}");