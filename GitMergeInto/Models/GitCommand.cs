using System.Diagnostics;

namespace GitMergeInto.Models;

public static class GitCommand
{
    public static async Task<GitProcess> Run(params string?[] args)
    {
        var startInfo = new ProcessStartInfo("git", string.Join(" ", args))
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process { StartInfo = startInfo };
        process.Start();
        await process.WaitForExitAsync();

        return new GitProcess()
        {
            ExitCode = process.ExitCode,
            StandardOutput = await process.StandardOutput.ReadToEndAsync(),
            StandardError = await process.StandardError.ReadToEndAsync()
        };
    }

    public static async Task CheckUncommitted()
    {
        var diffProcess = await Run("diff", "--quiet");
        if (diffProcess.IsFailed())
            throw new GitCommandFailed("There are uncommitted changes in the current branch.");
    }

    public static async Task Checkout(string targetBranch)
    {
        await Run("checkout", targetBranch);
    }

    public static async Task Fetch(string targetBranch)
    {
        var fetchProcess = await Run("fetch", "origin", targetBranch);
        Console.Write(fetchProcess.StandardOutput);
        Console.Write(fetchProcess.StandardError);
        if (fetchProcess.IsFailed())
            throw new GitCommandFailed($"Failed to fetch the '{targetBranch}' branch.");
    }

    public static async Task ResetHard(string targetBranch)
    {
        var resetProcess = await Run("reset", "--hard", $"origin/{targetBranch}");
        if (resetProcess.IsFailed())
            throw new GitCommandFailed($"Failed to reset the '{targetBranch}' branch.");
    }

    public static async Task Merge(string originalBranch, string targetBranch)
    {
        var mergeProcess = await Run("merge", originalBranch);
        if (mergeProcess.IsFailed())
        {
            if (mergeProcess.GetTrimStandardOutput().Contains("CONFLICT"))
            {
                throw new GitCommandFailed($"Merge conflict detected for branch '{targetBranch}'.");
            }
            else
            {
                throw new GitCommandFailed($"Merge failed for branch '{targetBranch}'.");
            }
        }
    }

    public static async Task<string> GetOriginalBranch()
    {
        var originBranchProcess = await Run("branch", "--show-current");
        var originalBranch = originBranchProcess.GetTrimStandardOutput();
        return originalBranch;
    }
}