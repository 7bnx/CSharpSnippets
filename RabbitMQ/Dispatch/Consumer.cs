using CSharpSnippets.RabbitMQ.Common;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace CSharpSnippets.RabbitMQ.Dispatch
{
  public sealed class Consumer : IDisposable
  {
    public string QueueName { get; init; }
    private int _longWorkImitationInMsec;
    private readonly Action<string> _receiveAction;
    private bool _disposed;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private EventingBasicConsumer? _consumer;
    private int _consumerId;
    private static int _countCunsumers;
    public Consumer(string queueName, int longWorkImitationInMsec, Action<string> receiveAction)
    {
      QueueName = queueName;
      _consumerId = ++_countCunsumers;
      _longWorkImitationInMsec = longWorkImitationInMsec;
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
      _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
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
        autoAck: false,
        consumer: _consumer
      );
    }
    private void ReceiveHandler(object? sender, BasicDeliverEventArgs ea)
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Thread.Sleep(_longWorkImitationInMsec);
      _receiveAction?.Invoke($"{DateTime.Now} | Consumer {_consumerId} | Message received: {message}");
      ((EventingBasicConsumer)sender!).Model.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }

    public void Dispose()
    {
      if (_disposed) return;
      _disposed = true;
      _connection?.Dispose();
      _channel?.Dispose();
    }
    ~Consumer()
    {
      Dispose();
    }


  }
}
