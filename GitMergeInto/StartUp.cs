using GitMergeInto.Interfaces;
using GitMergeInto.Models;
using GitMergeInto.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GitMergeInto;

internal static class StartUp
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConsoleLogger, ConsoleLogger>();
        services.AddSingleton<GitMergeIntoService>();

        return services.BuildServiceProvider();
    }
}