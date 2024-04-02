using CSharpSnippets.MassTransit.Contracts;
using MassTransit;

namespace CSharpSnippets.MassTransit.MassProducer;
internal class MessageRequestConsumer : IConsumer<MessageRequest>
{
  public async Task Consume(ConsumeContext<MessageRequest> context)
  {
    int i = Random.Shared.Next();
    if (i % 2 == 0)
      await context.RespondAsync<MessageResponse>(new(Guid.NewGuid(), $"Response time {DateTime.UtcNow}"));
    else
      await context.RespondAsync<MessageResponseNotFound>(new());
  }
}