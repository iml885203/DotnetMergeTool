namespace GitMergeInto.Models;

public class GitProcess
{
    public int ExitCode { get; set; }
    public string? StandardOutput { get; set; }
    public string? StandardError { get; set; }

    public bool IsFailed()
    {
        return ExitCode != 0;
    }

    public string GetTrimStandardOutput()
    {
        return string.IsNullOrEmpty(StandardOutput) ? string.Empty : StandardOutput.Trim();
    }
}