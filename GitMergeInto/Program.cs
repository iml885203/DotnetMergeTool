using System.CommandLine;
using GitMergeInto.Models;

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

            rootCommand.SetHandler(HandleCommand, branchArgument);

            return await rootCommand.InvokeAsync(args);
        }

        private static async Task HandleCommand(string targetBranch)
        {
            var originalBranch = await GitCommand.GetOriginalBranch();

            try
            {
                await GitMergeInto(originalBranch, targetBranch.Trim());
            }
            catch (GitCommandFailed e)
            {
                ConsoleLogger.Error(e.ErrorMessage);
                await GitCommand.Run("checkout", originalBranch);
            }
        }

        private static async Task GitMergeInto(string originalBranch, string targetBranch)
        {
            if (originalBranch == targetBranch)
                throw new GitCommandFailed($"Cannot merge the '{originalBranch}' into '{targetBranch}' branch.");

            await GitCommand.CheckUncommitted();

            ConsoleLogger.Info($"Pulling changes from '{targetBranch}' branch...");
            await GitCommand.Checkout(targetBranch);
            await GitCommand.Fetch(targetBranch);
            await GitCommand.ResetHard(targetBranch);

            ConsoleLogger.Info($"Merging changes from current branch to '{targetBranch}' branch...");
            await GitCommand.Merge(originalBranch, targetBranch);
            await GitCommand.Checkout(originalBranch);

            ConsoleLogger.Success($"Merged changes from current branch to '{targetBranch}' branch.");
        }
    }
}