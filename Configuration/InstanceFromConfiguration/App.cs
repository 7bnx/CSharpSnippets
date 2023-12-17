using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = new();

Console.WriteLine("Init instance WITHOUT setting private property");
var instanceWithoutPrivateProperty = builder
                .Configuration
                .GetSection("ConfigurationClass")
                .Get<ConfigurationClass>();
Console.WriteLine(instanceWithoutPrivateProperty);
Console.WriteLine(Environment.NewLine);

Console.WriteLine("Init instance WITH setting private property");
var instanceWithPrivateProperty = builder
                .Configuration
                .GetSection("ConfigurationClass")
                .Get<ConfigurationClass>(binder => binder.BindNonPublicProperties = true);
Console.WriteLine(instanceWithPrivateProperty);



class ConfigurationClass
{
    public string Key { get; set; } = "Default";
    public InnerClass? InnerClass { get; set; }
    private string PrivateKey { get; set; } = "Default PrivateKey";
    public override string ToString()
    {
        return "Key = " + Key + ", InnerClass Key = " + InnerClass!.Key + ", Private Key = " + PrivateKey;
    }

}

public class InnerClass
{
    public string Key { get; set; } = "Default";
}