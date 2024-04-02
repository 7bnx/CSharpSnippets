using MassTransit;

class BusObserver : IBusObserver
{
  public void CreateFaulted(Exception exception)
  {
    Console.WriteLine("CreateFaulted " + DateTime.UtcNow);
  }

  public void PostCreate(IBus bus)
  {
    Console.WriteLine("PostCreate " + DateTime.UtcNow);
  }

  public Task PostStart(IBus bus, Task<BusReady> busReady)
  {
    Console.WriteLine("PostStart " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task PostStop(IBus bus)
  {
    Console.WriteLine("PostStop " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task PreStart(IBus bus)
  {
    Console.WriteLine("PreStart " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task PreStop(IBus bus)
  {
    Console.WriteLine("PreStop " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task StartFaulted(IBus bus, Exception exception)
  {
    Console.WriteLine("StartFaulted " + DateTime.UtcNow);
    return Task.CompletedTask;
  }

  public Task StopFaulted(IBus bus, Exception exception)
  {
    Console.WriteLine("StopFaulted " + DateTime.UtcNow);
    return Task.CompletedTask;
  }
}