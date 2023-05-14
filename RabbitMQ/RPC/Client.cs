using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSnippets.RabbitMQ.Common;
using System.Data.Common;
using System.Threading.Channels;

namespace CSharpSnippets.RabbitMQ.RPC
{
  public class RpcClient : IDisposable
  {
    public string QueueName{get;init;}
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private readonly IModel? _channel;
    private readonly string replyQueueName;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();
    private bool _disposed;
    public RpcClient(string queueName)
    {
      QueueName = queueName;
      _connectionFactory = new ConnectionFactory
      {
        HostName = Configuration.ConnectionFactory.HostName,
        Port = Configuration.ConnectionFactory.Port,
        UserName = Configuration.ConnectionFactory.UserName,
        Password = Configuration.ConnectionFactory.Password
      };
      _connection = _connectionFactory.CreateConnection();
      _channel = _connection.CreateModel();

      replyQueueName = _channel.QueueDeclare().QueueName;
      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (model, ea) =>
      {
        if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
          return;
        var body = ea.Body.ToArray();
        var response = Encoding.UTF8.GetString(body);
        tcs.TrySetResult(response);
      };

      _channel.BasicConsume
      (
        consumer: consumer,
        queue: replyQueueName,
        autoAck: true
       );
    }

    public Task<string> CallAsync(string message, CancellationToken cancellationToken = default)
    {
      IBasicProperties props = _channel!.CreateBasicProperties();
      var correlationId = Guid.NewGuid().ToString();
      props.CorrelationId = correlationId;
      props.ReplyTo = replyQueueName;
      var messageBytes = Encoding.UTF8.GetBytes(message);
      var tcs = new TaskCompletionSource<string>();
      callbackMapper.TryAdd(correlationId, tcs);

      _channel.BasicPublish
      (
        exchange: string.Empty,
        routingKey: QueueName,
        basicProperties: props,
        body: messageBytes
      );

      cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));
      return tcs.Task;
    }

    public void Dispose()
    {
      if (_disposed) return;
      _disposed = true;
      _channel?.Dispose();
      _connection?.Dispose();
    }
  }
}
