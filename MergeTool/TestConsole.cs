using MergeTool.Models;
using Spectre.Console;

public static class TestConsole
{
    public static void Main(string[] args)
    {
        var consoleLogger = new ConsoleLogger();
        consoleLogger.SetEnableVerbose(true);
        consoleLogger.Info("This is an info message.");
        consoleLogger.Error("This is an error message.");
        consoleLogger.Warning("This is a warning message.");
        consoleLogger.Success("This is a success message.");
        consoleLogger.Verbose("This is a verbose message.");
    }
}