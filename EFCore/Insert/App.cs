using CSharpSnippets.EFCore.Insert;
var dbName = "insert";
Insert insert = new(dbName);

Console.WriteLine("Delete current DB and insert some values");
Console.WriteLine();
Console.Write($"Count of inserted values: {insert.DeleteDBAndInsert()}");
Console.ReadKey();