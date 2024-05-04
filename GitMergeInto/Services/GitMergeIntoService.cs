using GitMergeInto.Models;

namespace GitMergeInto.Services;

public static class GitMergeIntoService
{
    public static async Task GitMergeInto(string targetBranch)
    {
        var originalBranch = await GitCommand.GetOriginalBranch();

        try
        {
            await GitMergeIntoFlow(originalBranch, targetBranch.Trim());
        }
        catch (GitCommandFailed e)
        {
            ConsoleLogger.Error(e.ErrorMessage);
            await GitCommand.Run("checkout", originalBranch);
        }
    }

    private static async Task GitMergeIntoFlow(string originalBranch, string targetBranch)
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