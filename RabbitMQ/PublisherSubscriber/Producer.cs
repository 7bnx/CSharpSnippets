using CSharpSnippets.RabbitMQ.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnippets.RabbitMQ.PublisherSubscriber
{
  public sealed class Producer : IDisposable
  {
    public string ExchangeName { get; init; }
    private bool _disposed;
    private ConnectionFactory? _connectionFactory;
    private IConnection? _connection;
    private IModel? _channel;
    private Timer? _timer;
    private int _messageCounter;
    public Producer(string exchangeName)
    {
      ExchangeName = exchangeName;
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
      _channel?.Dispose();
      _connection?.Dispose();
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
      _channel.ExchangeDeclare
      (
        exchange: ExchangeName,
        type: ExchangeType.Fanout,
        durable: false,
        autoDelete: false
      );
    }

    private void SendMessage(object? _)
    {
      string message = $"number {++_messageCounter}, dateTime {DateTime.Now}";
      var body = Encoding.UTF8.GetBytes(message);
      _channel!.BasicPublish
      (
        exchange: ExchangeName,
        routingKey: string.Empty,
        basicProperties: null,
        body: body
      );
    }
  }
}
