using CSharpSnippets.RabbitMQ.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CSharpSnippets.RabbitMQ.RPC
{
  public sealed class RpcServer : IDisposable
  {
    public string QueueName { get; init; }
    private bool _disposed;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private EventingBasicConsumer? _consumer;
    public RpcServer(string queueName)
    {
      QueueName = queueName;
    }

    public void Start()
    {
      InitRabbit();
    }
    public void Dispose()
    {
      if (_disposed) return;
      _disposed = true;
      _channel?.Dispose();
      _connection?.Dispose();
    }
    ~RpcServer()
    {
      Dispose();
    }

    private void InitRabbit()
    {
      _connectionFactory = new ConnectionFactory
      {
        HostName = Configuration.ConnectionFactory.HostName,
        Port = Configuration.ConnectionFactory.Port,
        UserName = Configuration.ConnectionFactory.UserName,
        Password = Configuration.ConnectionFactory.Password
      };
      _connection = _connectionFactory.CreateConnection();
      _channel = _connection.CreateModel();

      _channel.QueueDeclare
      (
        queue: QueueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
      );
      _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
      _consumer = new EventingBasicConsumer(_channel);

      _channel.BasicConsume
      (
        queue: QueueName,
        autoAck: false,
        consumer: _consumer
      );

      _consumer.Received += ConsumerReceived;
    }

    private void ConsumerReceived(object? sender, BasicDeliverEventArgs ea)
    {
      string response = string.Empty;
      var body = ea.Body.ToArray();
      var props = ea.BasicProperties;
      var replyProps = _channel!.CreateBasicProperties();
      replyProps.CorrelationId = props.CorrelationId;

      try
      {
        var message = Encoding.UTF8.GetString(body);
        int n = int.Parse(message);
        response = Fib(n).ToString();
      } catch (Exception)
      {
        response = string.Empty;
      } finally
      {
        var responseBytes = Encoding.UTF8.GetBytes(response);
        _channel.BasicPublish
        (
          exchange: string.Empty,
          routingKey: props.ReplyTo,
          basicProperties: replyProps,
          body: responseBytes
        );
        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
      }
    }

    static int Fib(int n)
    {
      if (n is 0 or 1)
      {
        return n;
      }

      return Fib(n - 1) + Fib(n - 2);
    }
  }
}
