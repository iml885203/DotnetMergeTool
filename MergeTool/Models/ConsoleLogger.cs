using MergeTool.Interfaces;
using Spectre.Console;

namespace MergeTool.Models;

public class ConsoleLogger : IConsoleLogger
{
    private bool _enableVerbose = false;

    public void Info(string message)
    {
        AnsiConsole.MarkupLine($"[aqua][[Info]] {message}[/]");
    }

    public void Warning(string message)
    {
        AnsiConsole.MarkupLine($"[yellow][[Warning]] {message}[/]");
    }

    public void Error(string message)
    {
        AnsiConsole.MarkupLine($"[red][[Error]] {message}[/]");
    }

    public void Success(string message)
    {
        AnsiConsole.MarkupLine($"[green][[Success]] {message}[/]");
    }

    public void Verbose(string message)
    {
        if (!_enableVerbose) return;
        AnsiConsole.MarkupLine($"[gray][[Verbose]] {message}[/]");
    }

    public void SetEnableVerbose(bool enabled)
    {
        _enableVerbose = enabled;
    }
}