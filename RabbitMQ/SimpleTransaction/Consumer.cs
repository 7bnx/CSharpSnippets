using CSharpSnippets.RabbitMQ.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CSharpSnippets.RabbitMQ.SimpleTransaction
{
  public sealed class Consumer : IDisposable
  {
    public string QueueName { get; init; }
    private readonly Action<string> _receiveAction;
    private bool _disposed;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private EventingBasicConsumer? _consumer;

    public Consumer(string queueName, Action<string> receiveAction)
    {
      QueueName = queueName;
      _receiveAction = receiveAction;
    }
    public void Start()
    {
      InitRabbit();
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
      _consumer = new EventingBasicConsumer(_channel);
      _consumer.Received += ReceiveHandler;
      _channel.BasicConsume
      (
        queue: QueueName,
        autoAck: true,
        consumer: _consumer
      );
    }
    private void ReceiveHandler(object? sender, BasicDeliverEventArgs ea)
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      _receiveAction?.Invoke($"{DateTime.Now} | Message received: {message}");
    }

    public void Dispose()
    {
      if (_disposed) return;
      _disposed = true;
      _channel?.Dispose();
      _connection?.Dispose();
    }
    ~Consumer()
    {
      Dispose();
    }


  }
}
