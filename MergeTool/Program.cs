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

            var rootCommand = new RootCommand("Merge Tool");

            var branchArgument = new Argument<string>
            (
                name: "branch",
                description: "The branch argument"
            );
            var verboseOption = new Option<bool>(
                aliases: ["-v", "--verbose"],
                description: "Show verbose output"
            );

            var gmiCommand = new Command("gmi", "Merge into the target branch")
            {
                branchArgument,
                verboseOption
            };
            gmiCommand.SetHandler(mergeToolService.GitMergeInto, branchArgument, verboseOption);
            rootCommand.Add(gmiCommand);

            var gmipCommand = new Command("gmip", "Merge into the target branch and push the changes")
            {
                branchArgument,
                verboseOption
            };
            gmipCommand.SetHandler(mergeToolService.GitMergeIntoPush, branchArgument, verboseOption);
            rootCommand.Add(gmipCommand);

            return await rootCommand.InvokeAsync(args);
        }
    }
}