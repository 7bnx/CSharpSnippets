using CSharpSnippets.RabbitMQ.RPC;

const string queueName = "RPCQueue";
using var rpcServer = new RpcServer(queueName);
rpcServer.Start();
using var rpcClient = new RpcClient(queueName);

CallProcedure();

Console.WriteLine("Press any key to Stop\n");
Console.ReadKey();

void CallProcedure()
{
  Task.Run(async () => 
  {
    var random = new Random();
    while (true)
    {
      await Task.Delay(1000);
      int fib = random.Next(1, 42);
      Console.WriteLine($"Fibonacci seq: {fib}");
      var response = await rpcClient!.CallAsync($"{fib}");
      Console.WriteLine($"Response num: {response}\n");
    }
  });
}