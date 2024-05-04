namespace GitMergeInto.Models;

public class GitCommandFailed(string message) : Exception
{
    public string ErrorMessage { get; set; } = message;
}