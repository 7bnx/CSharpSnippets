using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
{
    Console.WriteLine(hostContext.HostingEnvironment.EnvironmentName);
    services.Configure<Options>(hostContext.Configuration.GetSection("ParametersSection"));
    services.AddTransient<Service>();
}).Build();



var service = host.Services.GetRequiredService<Service>();

Console.WriteLine(service);

Console.ReadLine();

class Service
{
    private readonly IOptions<Options> _options;

    public Service(IOptions<Options> options)
    {
        _options = options;
    }

    public override string ToString()
    {
        return "Parameter value = " + _options.Value.Parameter!;
    }
}

class Options
{
    public string Parameter { get; set; } = "Default Parameter";
}