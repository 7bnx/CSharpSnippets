using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

await Host.CreateDefaultBuilder()
  .ConfigureLogging((context, logBuilder) =>
  {
      logBuilder.ClearProviders();
      logBuilder.AddNLog(context.Configuration);
  })
  .ConfigureServices((context, services) =>
  {
      services.Configure<ServiceOptions>(context.Configuration.GetSection("ServiceOptions"));
      services.AddSingleton<Service>();
      services.AddHostedService<Worker>();
  })
  .ConfigureAppConfiguration((context, config) =>
  {
  }).RunConsoleAsync();

class Worker : IHostedService
{
    private readonly Service _service;
    private readonly IHostApplicationLifetime _hostLifetime;
    private readonly ILogger _logger;
    private int? _exitCode;

    public Worker(Service service, IHostApplicationLifetime hostLifetime, ILogger<Worker> logger)
    {
        _service = service;
        _hostLifetime = hostLifetime;
        _logger = logger;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker Started");
        try
        {
            await _service.OperationAsync();
            _exitCode = 0;
        }
        catch (Exception ex)
        {
            _exitCode = 1;
        }
        finally
        {
            _hostLifetime.StopApplication();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        _logger.LogInformation("Worker stopped");
        return Task.CompletedTask;
    }
}

class Service
{
    private readonly IOptions<ServiceOptions> _options;

    public Service(IOptions<ServiceOptions> options)
    {
        _options = options;
    }
    public async Task OperationAsync()
    {
        await Task.Delay(_options.Value.DelayTime);
        Console.WriteLine(_options.Value.Parameter);
    }
}

class ServiceOptions
{
    public int DelayTime { get; set; }
    public string? Parameter { get; set; }
}