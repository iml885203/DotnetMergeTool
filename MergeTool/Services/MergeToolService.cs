using MergeTool.Interfaces;
using MergeTool.Models;

namespace MergeTool.Services;

public class MergeToolService(IConsoleLogger consoleLogger)
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

    public async Task GitMergeIntoPush(string targetBranch)
    {
        var originalBranch = await GitCommand.GetOriginalBranch();

        try
        {
            await GitMergeIntoPushFlow(originalBranch, targetBranch.Trim());
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

    private async Task GitMergeIntoPushFlow(string originalBranch, string targetBranch)
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

        consoleLogger.Info($"Pushing changes to '{targetBranch}' branch...");
        consoleLogger.Verbose(await GitCommand.Push(targetBranch));
        await GitCommand.Checkout(originalBranch);

        consoleLogger.Success($"Merged the '{originalBranch}' branch into '{targetBranch}' branch.");
    }
}