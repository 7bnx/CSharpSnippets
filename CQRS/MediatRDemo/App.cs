using System.Reflection;
using CSharpSnippets.CQRS.MediatRDemo;
using CSharpSnippets.CQRS.MediatRDemo.Application;
using CSharpSnippets.CQRS.MediatRDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var hostBuilder = Host.CreateDefaultBuilder();
hostBuilder.ConfigureServices(serv =>
{
  serv.AddDbContextFactory<ProductContext>((pp, opt) =>
  {

    opt.UseSqlite("Data Source=MediatRTestDB.db");
  });
  serv.AddSingleton<ApplicationService>();
  serv.AddSingleton<Repository>();
  serv.AddHostedService<Worker>();
  serv.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
});

var host = hostBuilder.Build();

await host.RunAsync();