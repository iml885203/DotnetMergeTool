using System.CommandLine;
using MergeTool.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MergeTool
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var serviceProvider = StartUp.ConfigureServices();

            var branchArgument = new Argument<string>
            (
                name: "branch",
                description: "The branch argument"
            );

            var rootCommand = new RootCommand { branchArgument };

            var gitMergeIntoService = serviceProvider.GetService<MergeToolService>()!;
            rootCommand.SetHandler(gitMergeIntoService.GitMergeInto, branchArgument);


            return await rootCommand.InvokeAsync(args);
        }
    }
}