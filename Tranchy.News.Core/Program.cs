using Blog_microkernel_in_action;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((hostContext, services) =>
{
    PluginLoader.LoadPlugins(Directory.GetCurrentDirectory(), services);
    services.AddHostedService<NewsProcessor>();
});