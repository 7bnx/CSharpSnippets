using CSharpSnippets.CQRS.MediatRDemo.Application;
using CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CSharpSnippets.CQRS.MediatRDemo;
class Worker
(
  ApplicationService service,
  IHostApplicationLifetime hostLifetime,
  IDbContextFactory<ProductContext> contextFactory,
  ILogger<Worker> logger
) : IHostedService
{
  private readonly ApplicationService _service = service;
  private readonly IHostApplicationLifetime _hostLifetime = hostLifetime;
  private readonly IDbContextFactory<ProductContext> _contextFactory = contextFactory;
  private int? _exitCode;
  private readonly ILogger<Worker> _logger = logger;

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    try
    {
      await Init();
      await _service.OperationAsync();
      _exitCode = 0;
    }
    catch (Exception ex)
    {
      _exitCode = 1;
      _logger.LogCritical(ex.Message);
    }
    finally
    {
      _hostLifetime.StopApplication();
    }
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
    return Task.CompletedTask;
  }

  private async Task Init()
  {
    await using var context = await _contextFactory.CreateDbContextAsync();
    await context.Database.EnsureCreatedAsync();
    await context.DisposeAsync();
  }

}