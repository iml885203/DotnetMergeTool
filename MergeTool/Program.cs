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
            var mergeToolService = serviceProvider.GetService<MergeToolService>()!;

            var branchArgument = new Argument<string>
            (
                name: "branch",
                description: "The branch argument"
            );

            var rootCommand = new RootCommand("Merge Tool");

            var gmiCommand = new Command("gmi", "Merge into the target branch")
            {
                branchArgument
            };
            gmiCommand.SetHandler(mergeToolService.GitMergeInto, branchArgument);
            rootCommand.Add(gmiCommand);

            var gmipCommand = new Command("gmip", "Merge into the target branch and push the changes")
            {
                branchArgument
            };
            gmipCommand.SetHandler(mergeToolService.GitMergeIntoPush, branchArgument);
            rootCommand.Add(gmipCommand);

            return await rootCommand.InvokeAsync(args);
        }
    }
}