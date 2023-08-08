using CSharpSnippets.Dapper.Demo;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

const string dbName = "DapperDemo.db";
const string connectionString = $"Data Source={dbName}";

DefaultTypeMap.MatchNamesWithUnderscores = true;


using (IDbConnection db =  new SqliteConnection(connectionString))
{
  var countUsers = db.ExecuteScalar<int>("SELECT COUNT(*) FROM USERS");
  Console.WriteLine($"Count of Users before operations: {countUsers}");
  var countAddresses = db.ExecuteScalar<int>("SELECT COUNT(*) FROM ADDRESSES");
  Console.WriteLine($"Count of Addresses before operations: {countAddresses}");
  Console.WriteLine();
}

using (IDbConnection db =  new SqliteConnection(connectionString))
{
  var user = new User
  {
    Age = 1,
    Name = "User 1",
  };
  var ids = db.Query<int>("INSERT INTO USERS (AGE, Name) VALUES(@Age, @Name) returning UserID", user);
  Console.WriteLine("Add single user");
  Console.WriteLine($"Inserted: {string.Join(", ", ids)}");
  Console.WriteLine();
}

using (IDbConnection db =  new SqliteConnection(connectionString))
{
  var address1 = new Address
  {
    Name = "Address 1"
  };  
  var address2 = new Address
  {
    Name = "Address 2"
  };
  var list = new List<Address>() { address1, address2 };
  Console.WriteLine("Add several addresses");
  foreach (var address in list) 
    Console.WriteLine($"\t{address}");
  var countInserted = db.Execute("INSERT INTO ADDRESSES (Name) VALUES(@Name)", list);
  Console.WriteLine($"Inserted: {countInserted}");
  Console.WriteLine();
}

using (IDbConnection db =  new SqliteConnection(connectionString))
{
  var addresses = db.Query<Address>("SELECT * FROM ADDRESSES");
  var users = db.Query<User>("SELECT * FROM USERS");
  db.Execute("UPDATE USERS SET ADDRESSID = @AddressId WHERE USERID = @UserId", new { AddressId = addresses.First().AddressId, UserId = users.First().UserId });
}

using (IDbConnection db =  new SqliteConnection(connectionString))
{
  Console.WriteLine("Select users with address");
  var users = db.Query<User,Address, User>("SELECT * FROM USERS u JOIN ADDRESSES a ON a.ADDRESSID = u.ADDRESSID", (a, b) => { a.Address = b; return a; }, splitOn: "ADDRESSID");
  Console.WriteLine($"Selected users:");
  foreach (var user in users)
    Console.WriteLine($"\t{user}");
  Console.WriteLine();
}

using (IDbConnection db =  new SqliteConnection(connectionString))
{
  Console.WriteLine("Select addresses");
  var addresses = db.Query<User>("SELECT * From Addresses");
  Console.WriteLine($"Selected addresses:");
  foreach (var address in addresses)
    Console.WriteLine($"\t{address}");
  Console.WriteLine();
}

using (IDbConnection db =  new SqliteConnection(connectionString))
{
  Console.WriteLine("Delete all users");
  var countDeleted = db.Execute("DELETE FROM users");
  Console.WriteLine($"Deleted users: {countDeleted}");
  Console.WriteLine();
}

using (IDbConnection db = new SqliteConnection(connectionString))
{
  Console.WriteLine("Delete addresses by Name");
  string name = "Address";
  var countDeleted = db.Execute("DELETE FROM Addresses WHERE Name LIKE @Name", new {Name = $"%{name}%"});
  Console.WriteLine($"Deleted addresses: {countDeleted}");
  Console.WriteLine();
}