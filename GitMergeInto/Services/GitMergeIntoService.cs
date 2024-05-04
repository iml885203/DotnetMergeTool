using GitMergeInto.Interfaces;
using GitMergeInto.Models;

namespace GitMergeInto.Services;

public class GitMergeIntoService(IConsoleLogger consoleLogger)
{
    public async Task GitMergeInto(string targetBranch)
    {
        var originalBranch = await GitCommand.GetOriginalBranch();

        try
        {
            await GitMergeIntoFlow(originalBranch, targetBranch.Trim());
        }
        catch (GitCommandFailed e)
        {
            consoleLogger.Error(e.ErrorMessage);
            await GitCommand.Run("checkout", originalBranch);
        }
    }

    private async Task GitMergeIntoFlow(string originalBranch, string targetBranch)
    {
        if (originalBranch == targetBranch)
            throw new GitCommandFailed($"Cannot merge the '{originalBranch}' into '{targetBranch}' branch.");

        await GitCommand.CheckUncommitted();

        consoleLogger.Info($"Pulling changes from '{targetBranch}' branch...");
        await GitCommand.Checkout(targetBranch);
        consoleLogger.Verbose(await GitCommand.Fetch(targetBranch));
        consoleLogger.Verbose(await GitCommand.ResetHard(targetBranch));

        consoleLogger.Info($"Merging changes from current branch to '{targetBranch}' branch...");
        consoleLogger.Verbose(await GitCommand.Merge(originalBranch, targetBranch));
        await GitCommand.Checkout(originalBranch);

        consoleLogger.Success($"Merged the '{originalBranch}' branch into '{targetBranch}' branch.");
    }
}