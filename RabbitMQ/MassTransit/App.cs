using MassTransit;
using MassTransit.Testing;


namespace MassTransitExample
{

  public class IMessage
  {
    public string Text { get; set; }
    public DateTime Date { get; set; }
  }

  public class Message1 : IMessage
  {
    public string Text { get; set; }
  }
  public class Message2 : IMessage
  {
    public string Text { get; set; }
  }

  public class MessageConsumer1 : IConsumer<IMessage>
  {
    public async Task Consume(ConsumeContext<IMessage> context)
    {
      await Task.Delay(3000);
      Console.WriteLine($"1 ({Thread.CurrentThread.ManagedThreadId}) | {context.Message.Date : mm-ss-ffff} | Received message: {context.Message.Text}");
      await Task.CompletedTask;
    }
  }

  public class MessageConsumer2 : IConsumer<IMessage>
  {
    public async Task Consume(ConsumeContext<IMessage> context)
    {
      await Task.Delay(1000);
      Console.WriteLine($"2 ({Thread.CurrentThread.ManagedThreadId}) | {context.Message.Date : mm-ss-ffff}| Received message: {context.Message.Text}");
      await Task.CompletedTask;
    }
  }

  class Program
  {
    static async Task Main(string[] args)
    {
      var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
      {
        cfg.Host("192.168.1.24", 5672, "Test", h =>
        {
          h.Username("guest");
          h.Password("guest");
        });
        cfg.ReceiveEndpoint("message_queue", e =>
        {
          e.Consumer<MessageConsumer1>();
        });
      });

      await busControl.StartAsync();

      var busControl2 = Bus.Factory.CreateUsingRabbitMq(cfg =>
      {
        cfg.Host("192.168.1.24", 5672, "Test", h =>
        {
          h.Username("guest");
          h.Password("guest");
        });
        cfg.ReceiveEndpoint("message_queue", e =>
        {
          e.Consumer<MessageConsumer2>();
        });
      });

      await busControl2.StartAsync();

      try
      {
        Console.WriteLine("Enter a message to send (or 'exit' to quit):");
        int i = 0;
        while (true)
        {
          //var input = Console.ReadLine();

          //if (input == "exit")
            //break;

          var message = new IMessage { Text = $"{++i}", Date = DateTime.Now};
          await busControl.Publish(message);
          await Task.Delay(100);
        }
      } finally
      {
        await busControl.StopAsync();
      }
    }
  }
}


/*
namespace MassTransitExample
{
  public class Message
  {
    public string Text { get; set; }
  }

  public class MessageConsumer : IConsumer<Message>
  {
    public Task Consume(ConsumeContext<Message> context)
    {
      Console.WriteLine($"Received message: {context.Message.Text}");
      return Task.CompletedTask;
    }
  }

  class Program
  {
    static async Task Main(string[] args)
    {
      var busControl = Bus.Factory.CreateUsingInMemory(cfg =>
      {
        cfg.ReceiveEndpoint("message_queue", e =>
        {
          e.Consumer<MessageConsumer>();
        });
      });

      await busControl.StartAsync();

      try
      {
        Console.WriteLine("Enter a message to send (or 'exit' to quit):");

        while (true)
        {
          var input = Console.ReadLine();

          if (input == "exit")
            break;

          var message = new Message { Text = input };
          await busControl.Publish(message);
        }
      } finally
      {
        await busControl.StopAsync();
      }
    }
  }
}

/*

Console.WriteLine("12");

ServiceCollection services = new ServiceCollection();
ConfigureServices(services);
var provider = services.BuildServiceProvider();
var t = provider.GetTestHarness();
await t.Bus.Send(1);

void ConfigureServices(IServiceCollection services)
{
  services.AddMassTransit();

  services.AddSingleton(p => Bus.Factory.CreateUsingRabbitMq(cfg =>
  {
    cfg.Host("http://192.168.1.24",5672, "Test", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
  }));

  //services.AddSingleton<IBus>(p => p.GetRequiredService<IBusControl>());
  //services.AddSingleton<IHostedService, BusService>();
}

public class Message
{
  public string Text { get; set; }
}

public class MessageConsumer : IConsumer<Message>
{
  public Task Consume(ConsumeContext<Message> context)
  {
    Console.WriteLine($"Received message: {context.Message.Text}");
    return Task.CompletedTask;
  }
}
*/