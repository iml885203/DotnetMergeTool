using MergeTool.Interfaces;
using MergeTool.Models;
using MergeTool.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MergeTool;

internal static class StartUp
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConsoleLogger, ConsoleLogger>();
        services.AddSingleton<MergeToolService>();

        return services.BuildServiceProvider();
    }
}