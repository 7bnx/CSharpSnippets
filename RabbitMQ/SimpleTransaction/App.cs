using CSharpSnippets.RabbitMQ.SimpleTransaction;

const string queueName = "SimpleTransaction";
const int periodInMsec = 1000;
using var producer = new Producer(queueName);
producer.Start(periodInMsec);
Console.WriteLine("Press any key to stop");
Console.ReadKey();