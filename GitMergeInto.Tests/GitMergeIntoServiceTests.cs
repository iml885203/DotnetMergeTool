using System.Diagnostics;
using GitMergeInto.Interfaces;
using GitMergeInto.Services;
using NSubstitute;

namespace GitMergeInto.Tests;

[TestFixture]
public class GitMergeIntoServiceTests
{
    private const string CurrentBranch = "develop";
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
    public void should_be_merge_into()
    {
        //
    }

    [Test]
    public async Task when_same_branch_should_throw_error()
    {
        await _gitMergeIntoService.GitMergeInto(CurrentBranch);

        _consoleLogger.Received().Error($"Cannot merge the '{CurrentBranch}' into '{CurrentBranch}' branch.");
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

        await RunGitCommand("checkout -b develop");
    }

    private static async Task RunGitCommand(string command)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        var process = new Process()
        {
            StartInfo = startInfo
        };
        process.Start();
        await process.WaitForExitAsync();
        var stdout = await process.StandardOutput.ReadToEndAsync();
        if (!string.IsNullOrWhiteSpace(stdout))
            Console.Write(stdout);

        var stderr = await process.StandardError.ReadToEndAsync();
        if (!string.IsNullOrWhiteSpace(stderr))
            Console.Write(stderr);
    }
}