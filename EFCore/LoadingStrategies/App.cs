// See https://aka.ms/new-console-template for more information
using CSharpSnippets.EFCore.Insert;
var dbName = "loadingStrategies";

Console.WriteLine("Hello, World!");
Insert insert = new();
insert.DeleteDBAndInsert();