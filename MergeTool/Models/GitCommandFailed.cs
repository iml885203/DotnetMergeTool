namespace MergeTool.Models;

public class GitCommandFailed(string message, string? output = null) : Exception
{
    public string ErrorMessage { get; set; } = message;
    public string? Output { get; set; } = output;
}