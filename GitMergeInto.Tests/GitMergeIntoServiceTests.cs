using System.Diagnostics;
using GitMergeInto.Interfaces;
using GitMergeInto.Services;
using NSubstitute;

namespace GitMergeInto.Tests;

[TestFixture]
public class GitMergeIntoServiceTests
{
    private const string OriginalBranch = "develop";
    private const string TargetBranch = "main";
    private IConsoleLogger _consoleLogger;
    private GitMergeIntoService _gitMergeIntoService;
    private string _localRepoDirectory;
    private string _previousWorkingDirectory;
    private string _sandboxDirectory;

    [SetUp]
    public async Task SetUp()
    {
        await GivenGitSandbox();

        _consoleLogger = Substitute.For<IConsoleLogger>();
        _gitMergeIntoService = new GitMergeIntoService(_consoleLogger);
    }

    [TearDown]
    public void TearDown()
    {
        RemoveGitSandbox();
    }

    [Test]
    public async Task when_uncommitted()
    {
        await RunCommand("touch", "file.txt");

        await _gitMergeIntoService.GitMergeInto(TargetBranch);

        _consoleLogger.Received().Error("There are uncommitted changes in the current branch.");
    }

    [Test]
    public async Task when_same_branch()
    {
        await _gitMergeIntoService.GitMergeInto(OriginalBranch);

        _consoleLogger.Received().Error($"Cannot merge the '{OriginalBranch}' into '{OriginalBranch}' branch.");
    }

    [Test]
    public async Task when_fetch_fail()
    {
        const string targetBranch = "non_existent";
        await _gitMergeIntoService.GitMergeInto(targetBranch);

        _consoleLogger.Received().Info($"Pulling changes from '{targetBranch}' branch...");
        _consoleLogger.Received().Error($"Failed to fetch the '{targetBranch}' branch.");
    }

    [Test]
    public async Task when_merge_conflict()
    {
        const string fileName = "file.txt";
        await GivenFileOnTargetBranch(fileName, "Hello, World!");
        await GivenFileOnOriginalBranch(fileName, "This is a conflict!");

        await _gitMergeIntoService.GitMergeInto(TargetBranch);

        _consoleLogger.Received().Info($"Pulling changes from '{TargetBranch}' branch...");
        _consoleLogger.Received().Error($"Merge conflict detected for branch '{TargetBranch}'.");
    }

    [Test]
    public async Task should_be_merge_into_target()
    {
        await GivenFileOnOriginalBranch("new-file.txt", "This is a new file!");

        await _gitMergeIntoService.GitMergeInto(TargetBranch);

        _consoleLogger.Received().Info($"Pulling changes from '{TargetBranch}' branch...");
        _consoleLogger.Received().Info($"Merging changes from current branch to '{TargetBranch}' branch...");
        _consoleLogger.Received().Success($"Merged the '{OriginalBranch}' branch into '{TargetBranch}' branch.");
    }

    private static async Task GivenFileOnOriginalBranch(string fileName, string content)
    {
        await File.WriteAllTextAsync(fileName, content);
        await RunCommand("git", "add", fileName);
        await RunCommand("git", "commit -m 'AddFile'");
    }

    private static async Task GivenFileOnTargetBranch(string fileName, string content)
    {
        await RunCommand("git", "checkout", TargetBranch);
        await File.WriteAllTextAsync(fileName, content);
        await RunCommand("git", "add", fileName);
        await RunCommand("git", "commit -m 'AddFile'");
        await RunCommand("git", "push");
        await RunCommand("git", "checkout", OriginalBranch);
    }

    private void RemoveGitSandbox()
    {
        Directory.SetCurrentDirectory(_previousWorkingDirectory);

        if (Directory.Exists(_sandboxDirectory))
        {
            Directory.Delete(_sandboxDirectory, true);
        }

        if (Directory.Exists(_localRepoDirectory))
        {
            Directory.Delete(_localRepoDirectory, true);
        }
    }

    private async Task GivenGitSandbox()
    {
        _sandboxDirectory = Path.Combine(Path.GetTempPath(), "git_sandbox");
        Directory.CreateDirectory(_sandboxDirectory);
        await Process.Start("git", $"init --bare {_sandboxDirectory}").WaitForExitAsync();

        _localRepoDirectory = Path.Combine(Path.GetTempPath(), "local_repo");
        await Process.Start("git", $"clone {_sandboxDirectory} {_localRepoDirectory}").WaitForExitAsync();

        _previousWorkingDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(_localRepoDirectory);

        await RunCommand("git", "commit -m 'Init' --allow-empty");
        await RunCommand("git", "push");
        await RunCommand("git", "checkout -b develop");
    }

    private static async Task RunCommand(string command, params string?[] args)
    {
        Console.WriteLine($"> {command} " + string.Join(" ", args));
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = string.Join(" ", args),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        var process = new Process()
        {
            StartInfo = startInfo
        };
        process.Start();
        await process.WaitForExitAsync();
        var stdout = await process.StandardOutput.ReadToEndAsync();
        if (!string.IsNullOrWhiteSpace(stdout))
            Console.WriteLine(stdout);

        var stderr = await process.StandardError.ReadToEndAsync();
        if (!string.IsNullOrWhiteSpace(stderr))
            Console.WriteLine(stderr);
    }
}