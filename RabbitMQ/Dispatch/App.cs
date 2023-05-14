using CSharpSnippets.RabbitMQ.Dispatch;

const string queueName = "Dispatch";

Console.WriteLine($"Name of queue: {queueName}");
Console.Write("Set intrerval between messages (in msec): ");

int periodInMsec = int.Parse(Console.ReadLine()!);

using var producer = new Producer(queueName);
using var consumer1 = new Consumer
(
  queueName: queueName, 
  longWorkImitationInMsec: 2000, 
  receiveAction: Console.WriteLine
);

using var consumer2 = new Consumer
(
  queueName: queueName,
  longWorkImitationInMsec: 500,
  receiveAction: Console.WriteLine
);

producer.Start(periodInMsec);
consumer1.Start();
consumer2.Start();

Console.WriteLine("Press any key to stop");
Console.ReadKey();