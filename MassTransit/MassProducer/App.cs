using Contracts;
using CSharpSnippets.MassTransit.MassProducer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMassTransit(configure =>
{
  configure.SetKebabCaseEndpointNameFormatter();

  configure.AddConsumer<MessageRequestConsumer>();

  configure.UsingRabbitMq((context, rabbit) =>
  {
    rabbit.Host("192.168.1.24", 5672, "Test", h =>
    {
      h.Password("guest");
      h.Username("guest");
    });
    rabbit.ConfigureEndpoints(context);
  });
});

var provider = services.BuildServiceProvider();

CancellationTokenSource cts = new(TimeSpan.FromSeconds(10));

var massTransit = provider.GetService<IPublishEndpoint>()!;
var busControl = provider.GetService<IBusControl>()!;
await busControl.StartAsync(cts.Token);

while (!cts.IsCancellationRequested)
{
  Thread.Sleep(1000);
  await massTransit.Publish(new Message { Id = Guid.NewGuid() }, cts.Token);
}