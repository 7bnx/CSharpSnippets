using CSharpSnippets.EFCore.Insert;

Insert insert = new();

Console.WriteLine("Delete current DB and insert some values");
Console.WriteLine();
Console.Write($"Count of inserted values: {insert.DeleteDBAndInsert()}");
Console.ReadKey();