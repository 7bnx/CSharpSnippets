using CSharpSnippets.MongoDb.Demo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

const string connectionString = "mongodb://192.168.1.24:27017";
const string dbName = "Demo";
const string collectionName = "TestCollection";


var client = new MongoClient(connectionString);



using (var dbs = await client.ListDatabasesAsync())
{
  Console.WriteLine("Existed databases:");
  foreach (var item in dbs.ToList())
    Console.WriteLine($"\t{item}");
  Console.WriteLine();
}


Console.WriteLine($"Drop database {dbName}");
await client.DropDatabaseAsync(dbName);
Console.WriteLine();


Console.WriteLine($"Create database {dbName}");
var db = client.GetDatabase(dbName);
Console.WriteLine();

// Produce an exception System.ArgumentException: 'An item with the same key has already been added. 
// if placed after 'Get Collection'
BsonClassMap.RegisterClassMap<Person>(cm =>
{
  cm.AutoMap();
  cm.MapIdField(p => p.IdField); // define id
  cm.MapMember(p => p.Name).SetElementName("username"); // change stored name of key
  cm.MapMember(p => p.Company).SetIgnoreIfDefault(true);
  cm.MapMember(p => p.Languages).SetIgnoreIfNull(true);
});

Console.WriteLine($"Create collection {collectionName}");
await db.CreateCollectionAsync(collectionName);
var collection = db.GetCollection<Person>(collectionName);
//var collection = db.GetCollection<BsonDocument>(collectionName); //non-specified collection
Console.WriteLine();

Console.WriteLine($"Insert document");
var countElementsBeforeInsert = await collection.CountDocumentsAsync(new BsonDocument());
Console.WriteLine($"Count of documents before insert: {countElementsBeforeInsert}");
Person person1 = new() 
{ 
  Name = "Person 1", 
  Age = 38,
  Languages = new List<string>{ "en", "de" }
};

await collection.InsertOneAsync(person1);
var countElementsAfterInsert = await collection.CountDocumentsAsync(new BsonDocument());
Console.WriteLine($"Count of documents after insert: {countElementsAfterInsert}");
Console.WriteLine();

Console.WriteLine("Insert collection of documents");
await collection.InsertManyAsync(new[] 
{
  new Person(){ Name = "Person 2", Age = 44, Company = new (){ Name = "Company 1" } },
  new Person(){ Name = "Person 3", Age = 44, Company = new (){ Name = "Company 2" } },
  new Person(){ Name = "Person 4", Age = 21 }
});

var countElementsAfterInsertMany = await collection.CountDocumentsAsync(new BsonDocument());
Console.WriteLine($"Count of documents after insert: {countElementsAfterInsertMany}");
Console.WriteLine();


Console.WriteLine("List all documents");
using (var cursor = await collection.FindAsync(new BsonDocument()))
{
  foreach(var person in await cursor.ToListAsync())
    Console.WriteLine($"\t{person}");
  Console.WriteLine();
}

var ageFilter = 26;
Console.WriteLine($"Filter documents: age > {ageFilter} and company is not null");
var filterBuilder = Builders<Person>.Filter;
var filter = filterBuilder.Where(p => p.Age > ageFilter && p.Company != null);
// Equal expression
//var filter = filterBuilder.And(filterBuilder.Gt(p => p.Age, 25), filterBuilder.Ne(p => p.Company, null));
using (var cursor = await collection.FindAsync(filter))
{
  foreach (var person in await cursor.ToListAsync())
    Console.WriteLine($"\t{person}");
  Console.WriteLine();
}


Console.WriteLine($"Sorting by age ascending and name descending:");
var sortDefinition = Builders<Person>.Sort.Ascending(p => p.Age).Descending(p => p.Name);
var persons = await collection.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
foreach (var person in persons)
  Console.WriteLine($"\t{person}");
Console.WriteLine();


Console.WriteLine("Grouping by age");
var group = await collection
            .Aggregate()
            .Group
            (
              p => p.Age, //Group by age
              g => new { Age = g.Key, Names = g.Select(p => p.Name).ToList(), Count = g.Count()}) // new object
            .ToListAsync();
foreach (var item in group)
  Console.WriteLine(item.ToJson());
Console.WriteLine();


Console.WriteLine("Delete document");
var documentsBeforeDelete = await collection.CountDocumentsAsync(new BsonDocument());
Console.WriteLine($"Documents before delete: {documentsBeforeDelete}");
var resultDelete = await collection.DeleteManyAsync(p => p.Age == 44);
var documentsAfterDelete = await collection.CountDocumentsAsync(new BsonDocument());
Console.WriteLine($"Documents before delete: {documentsAfterDelete}");
Console.WriteLine($"Deleted documents: {resultDelete.DeletedCount}");
Console.WriteLine();


Console.WriteLine($"Insert another type to collection {collectionName}");
var collectionR = db.GetCollection<AnotherDocumentType>(collectionName);
AnotherDocumentType doc = new() { Name = "Name 1" };
collectionR.InsertOne(doc);
record AnotherDocumentType(string Name = "", ObjectId Id = new());
