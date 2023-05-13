using CSharpSnippets.RabbitMQ.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.RabbitMQ.Dispatch
{
  public sealed class Producer : IDisposable
  {
    public string QueueName { get; init; }
    private bool _disposed;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private Timer? _timer;
    private int _messageCounter;
    public Producer(string queueName)
    {
      QueueName = queueName;
    }

    public void Start(int periodInMsec)
    {
      if (_timer is not null) return;
      InitRabbit();
      _timer = new
      (
        state: null,
        dueTime: 0,
        period: periodInMsec,
        callback: SendMessage
      );
    }
    public void Dispose()
    {
      if (_disposed) return;
      _disposed = true;
      _timer?.Dispose();
      _connection?.Dispose();
      _channel?.Dispose();
    }
    ~Producer()
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
      _channel.QueuePurge(QueueName);
    }

    private void SendMessage(object? _)
    {
      string message = $"number {++_messageCounter}, dateTime {DateTime.Now}";
      var body = Encoding.UTF8.GetBytes(message);
      _channel!.BasicPublish
      (
        exchange: string.Empty,
        routingKey: QueueName,
        basicProperties: null,
        body: body
      );
    }
  }
}
