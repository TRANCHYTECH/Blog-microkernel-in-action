using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tranchy.News.Contract;

namespace Blog_microkernel_in_action;

public static class PluginLoader
{
    private const string PLUGIN_FOLDER = "Plugins";

    public static void LoadPlugins(string folderPath, IServiceCollection serviceCollection)
    {
        var dllFiles = Directory.GetFiles(Path.Combine(folderPath, PLUGIN_FOLDER), "*.dll");

        foreach (var dllFile in dllFiles)
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllFile);
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsAssignableFrom(typeof(INewsReaderPlugin)))
                    {
                        serviceCollection.AddScoped(typeof(INewsReaderPlugin), type);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error occured while loading plugin from dll {dllFile}");
            }
        }
    }
}
            