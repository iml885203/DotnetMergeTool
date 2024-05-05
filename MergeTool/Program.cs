using System.CommandLine;
using MergeTool.Models;
using MergeTool.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace MergeTool
{
    class Program
    {
        private static MergeToolService? _mergeToolService;

        private static readonly Argument<string> BranchArgument = new(
            name: "branch",
            getDefaultValue: () => string.Empty,
            description: "The branch argument"
        );

        private static readonly Option<bool> VerboseOption = new(
            aliases: ["-v", "--verbose"],
            description: "Show verbose output"
        );

        private static readonly Option<bool> PushOption = new(
            aliases: ["-p", "--push"],
            description: "Push the changes after merging"
        );

        public static async Task<int> Main(string[] args)
        {
            var serviceProvider = StartUp.ConfigureServices();
            _mergeToolService = serviceProvider.GetService<MergeToolService>()!;

            var rootCommand = new RootCommand("Merge Tool")
            {
                BranchArgument,
                PushOption,
                VerboseOption
            };

            rootCommand.SetHandler(HandleCommand, BranchArgument, PushOption, VerboseOption);

            return await rootCommand.InvokeAsync(args);
        }

        private static async Task HandleCommand(string targetBranch, bool needPush, bool showVerbose)
        {
            if (string.IsNullOrEmpty(targetBranch))
            {
                targetBranch = await PromptBranch();
            }

            if (needPush)
            {
                await _mergeToolService!.GitMergeIntoPush(targetBranch, showVerbose);
            }
            else
            {
                await _mergeToolService!.GitMergeInto(targetBranch, showVerbose);
            }
        }

        private static async Task<string> PromptBranch()
        {
            var originalBranch = await GitCommand.GetOriginalBranch();
            var localBranches = (await GitCommand.GetLocalBranches())
                .Where(branch => !string.IsNullOrEmpty(branch) && branch != originalBranch)
                .ToList();

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which branch do you want to merge into?")
                    .PageSize(5)
                    .MoreChoicesText("[grey](Move up and down to reveal more branch)[/]")
                    .AddChoices(localBranches));
        }
    }
}