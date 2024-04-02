using Contracts;
using MassTransit;

namespace CSharpSnippets.MassTransit.MassConsumer;
public class MessageConsumer : IConsumer<Message>
{
  public Task Consume(ConsumeContext<Message> context)
  {
    var message = context.Message;
    Console.WriteLine(message);
    return Task.CompletedTask;
  }
}