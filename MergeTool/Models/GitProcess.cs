namespace MergeTool.Models;

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

    public string GetOutput()
    {
        var output = string.IsNullOrEmpty(StandardOutput) ? string.Empty : StandardOutput;
        var error = string.IsNullOrEmpty(StandardError) ? string.Empty : StandardError;

        return $"{output}{error}";
    }
}