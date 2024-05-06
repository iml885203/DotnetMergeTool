using MergeTool.Interfaces;
using MergeTool.Models;

namespace MergeTool.Services;

public class MergeToolService(IConsoleLogger consoleLogger)
{
    private bool _verbose;

    public async Task GitMergeInto(string targetBranch, bool verbose = false)
    {
        var originalBranch = await GitCommand.GetOriginalBranch();

        try
        {
            await GitMergeIntoFlow(originalBranch, targetBranch.Trim());
        }
        catch (GitCommandFailed e)
        {
            if (!string.IsNullOrEmpty(e.Output))
            {
                consoleLogger.Warning(e.Output);
            }

            consoleLogger.Error(e.ErrorMessage);
            await GitCommand.Run("checkout", originalBranch);
        }
    }

    public async Task GitMergeIntoPush(string targetBranch, bool verbose = false)
    {
        var originalBranch = await GitCommand.GetOriginalBranch();

        try
        {
            await GitMergeIntoPushFlow(originalBranch, targetBranch.Trim());
        }
        catch (GitCommandFailed e)
        {
            if (!string.IsNullOrEmpty(e.Output))
            {
                consoleLogger.Warning(e.Output);
            }
            consoleLogger.Error(e.ErrorMessage);
            await GitCommand.Run("checkout", originalBranch);
        }
    }

    private async Task GitMergeIntoFlow(string originalBranch, string targetBranch)
    {
        if (originalBranch == targetBranch)
            throw new GitCommandFailed($"Cannot merge the '{originalBranch}' into '{targetBranch}' branch.");

        await GitCommand.CheckUncommitted();
        await GitCommand.CheckGitExists();
        await GitCommand.CheckBranchExists(targetBranch);

        consoleLogger.Info($"Pulling changes from '{targetBranch}' branch...");
        await GitCommand.Checkout(targetBranch);
        ConsoleVerbose(await GitCommand.Fetch(targetBranch));
        ConsoleVerbose(await GitCommand.ResetHard(targetBranch));

        consoleLogger.Info($"Merging changes from '{originalBranch}' to '{targetBranch}' branch...");
        ConsoleVerbose(await GitCommand.Merge(originalBranch, targetBranch));
        await GitCommand.Checkout(originalBranch);

        consoleLogger.Success($"Merged the '{originalBranch}' branch into '{targetBranch}' branch.");
    }

    private async Task GitMergeIntoPushFlow(string originalBranch, string targetBranch)
    {
        if (originalBranch == targetBranch)
            throw new GitCommandFailed($"Cannot merge the '{originalBranch}' into '{targetBranch}' branch.");

        await GitCommand.CheckUncommitted();
        await GitCommand.CheckGitExists();
        await GitCommand.CheckBranchExists(targetBranch);

        consoleLogger.Info($"Pulling changes from '{targetBranch}' branch...");
        await GitCommand.Checkout(targetBranch);
        var fetch = await GitCommand.Fetch(targetBranch);
        ConsoleVerbose(fetch);
        ConsoleVerbose(await GitCommand.ResetHard(targetBranch));

        consoleLogger.Info($"Merging changes from '{originalBranch}' to '{targetBranch}' branch...");
        ConsoleVerbose(await GitCommand.Merge(originalBranch, targetBranch));

        consoleLogger.Info($"Pushing changes to '{targetBranch}' branch...");
        ConsoleVerbose(await GitCommand.Push(targetBranch));
        await GitCommand.Checkout(originalBranch);

        consoleLogger.Success($"Merged the '{originalBranch}' branch into '{targetBranch}' branch.");
    }

    private void ConsoleVerbose(string? fetch)
    {
        if (_verbose)
        {
            consoleLogger.Verbose(fetch);
        }
    }

    public void EnableVerbose(bool showVerbose)
    {
        _verbose = showVerbose;
    }
}