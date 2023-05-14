using CSharpSnippets.RabbitMQ.SimpleTransaction;

const string queueName = "SimpleTransaction";

Console.WriteLine($"Name of queue: {queueName}");
Console.Write("Set intrerval between messages (in msec): ");

int periodInMsec = int.Parse(Console.ReadLine()!);

using var producer = new Producer(queueName);
using var consumer = new Consumer(queueName, Console.WriteLine);

producer.Start(periodInMsec);
consumer.Start();

Console.WriteLine("Press any key to stop");
Console.ReadKey();