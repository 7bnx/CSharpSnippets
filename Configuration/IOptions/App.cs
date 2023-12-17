using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
{
    services.Configure<Options>(opt => opt.Parameter = "Injected parameter");
    services.AddTransient<Service>();
}).Build();

var service = host.Services.GetRequiredService<Service>();

Console.WriteLine(service);

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
    public string? Parameter { get; set; }
}