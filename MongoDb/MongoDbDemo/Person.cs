using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.MongoDb.Demo;
record Person
{
  [BsonId]
  public ObjectId IdField { get; set; }
  public string? Name { get; set; }
  public int Age { get; set; }
  public Company? Company { get; set; }
  public List<string>? Languages { get; set; }
}
