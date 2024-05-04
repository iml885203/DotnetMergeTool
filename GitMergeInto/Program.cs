using System.CommandLine;
using GitMergeInto.Services;

namespace GitMergeInto
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var branchArgument = new Argument<string>
            (
                name: "branch",
                description: "The branch argument"
            );

            var rootCommand = new RootCommand { branchArgument };

            rootCommand.SetHandler(GitMergeIntoService.GitMergeInto, branchArgument);

            return await rootCommand.InvokeAsync(args);
        }
    }
}