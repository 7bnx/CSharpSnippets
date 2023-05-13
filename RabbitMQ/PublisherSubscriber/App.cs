using CSharpSnippets.RabbitMQ.PublisherSubscriber;

const string exchangeName = "PublishSubscriber";

Console.WriteLine($"Name of exchange: {exchangeName}");
Console.Write("Set intrerval between messages (in msec): ");

int periodInMsec = int.Parse(Console.ReadLine()!);

using var producer = new Producer(exchangeName);
using var consumer1 = new Consumer
(
  exchangeName: exchangeName,
  receiveAction: Console.WriteLine
);

using var consumer2 = new Consumer
(
  exchangeName: exchangeName,
  receiveAction: Console.WriteLine
);

consumer1.Start();
consumer2.Start();
producer.Start(periodInMsec);

Console.WriteLine("Press any key to stop");
Console.ReadKey();