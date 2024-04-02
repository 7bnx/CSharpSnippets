using CSharpSnippets.MassTransit.Contracts;
using CSharpSnippets.MassTransit.MassConsumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddReceiveEndpointObserver<EndpointObserver>();
services.AddBusObserver<BusObserver>();

services.AddMassTransit(configure =>
{
  configure.SetKebabCaseEndpointNameFormatter();

  configure.AddConsumer<MessageConsumer>();

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
var massTransit = provider.GetService<IBusControl>()!;
var requestClient = provider.GetService<IRequestClient<MessageRequest>>()!;


CancellationTokenSource cts = new(TimeSpan.FromSeconds(10));
var t1 = massTransit.StartAsync(cts.Token);
var t2 = requestClient.GetResponse<MessageResponse, MessageResponseNotFound>(new(Guid.NewGuid()), cts.Token);

await Task.WhenAll(t1, t2);

var response = t2.Result;

if (response.Is(out Response<MessageResponse>? successResponse))
{
  Console.WriteLine(successResponse.Message);
}
else if (response.Is(out Response<MessageResponseNotFound>? notFoundResponse))
{
  Console.WriteLine("Response not found");
}

cts.Cancel();