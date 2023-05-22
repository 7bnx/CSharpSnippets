using CSharpSnippets.Ado.Init;
using CSharpSnippets.Ado.Common;
using CSharpSnippets.Ado.Transaction;

Init init = new();
init.Drop();
init.Create();
init.Insert();

Transaction trans = new();
Console.WriteLine("Insert transaction was interrupted");
var resultObjects = trans.Start(
  interruptTransaction: true, 
  tableName: "Users", 
  columns: new string[] { "idUser", "firstName", "age" }, 
  values: new object[] { 8, "'Transaction'", 99});

PrintResult(resultObjects);

Console.WriteLine();

Console.WriteLine("Insert transaction not interrupted");
resultObjects = trans.Start(
  interruptTransaction: false, 
  tableName: "Users", 
  columns: new string[] { "idUser", "firstName", "age" }, 
  values: new object[] { 8, "'Transaction'", 99 });

PrintResult(resultObjects);

static void PrintResult(List<List<object>> list)
{
  Console.WriteLine($"Count Users: {list.Count}");
  foreach (var obj in list)
    Console.WriteLine(new User(obj));
}