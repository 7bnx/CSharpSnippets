using CSharpSnippets.RabbitMQ.Common;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace CSharpSnippets.RabbitMQ.PublisherSubscriber
{
  public sealed class Consumer : IDisposable
  {
    public string ExchangeName { get; init; }
    private readonly Action<string> _receiveAction;
    private bool _disposed;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private EventingBasicConsumer? _consumer;
    private readonly int _consumerId;
    private static int _countCunsumers;
    public Consumer(string exchangeName, Action<string> receiveAction)
    {
      ExchangeName = exchangeName;
      _consumerId = ++_countCunsumers;
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
      _channel.ExchangeDeclare
      (
        exchange: ExchangeName,
        type: ExchangeType.Fanout,
        durable: false,
        autoDelete: false
      );
      var queueName = _channel.QueueDeclare().QueueName;
      _channel.QueueBind
      (
        queue: queueName,
        exchange: ExchangeName,
        routingKey: string.Empty
      );
      _consumer = new EventingBasicConsumer(_channel);
      _consumer.Received += ReceiveHandler;
      _channel.BasicConsume
      (
        queue: queueName,
        autoAck: true,
        consumer: _consumer
      );
    }
    private void ReceiveHandler(object? sender, BasicDeliverEventArgs ea)
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      _receiveAction?.Invoke($"{DateTime.Now} | Consumer {_consumerId} | Message received: {message}");
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
