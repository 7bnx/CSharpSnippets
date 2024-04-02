using MassTransit;

class EndpointObserver : IReceiveEndpointObserver
{
  public Task Completed(ReceiveEndpointCompleted completed)
  {
    Console.WriteLine("Completed " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task Faulted(ReceiveEndpointFaulted faulted)
  {
    Console.WriteLine("Faulted " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task Ready(ReceiveEndpointReady ready)
  {
    Console.WriteLine("Ready " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task Stopping(ReceiveEndpointStopping stopping)
  {
    Console.WriteLine("Stopping " + DateTime.UtcNow);
    return Task.CompletedTask;
  }
}