using MergeTool.Enums;
using MergeTool.Interfaces;

namespace MergeTool.Models;

public class ConsoleLogger : IConsoleLogger
{
    private bool _enableVerbose = false;

    private void WriteMessage(string message, MessageType messageType)
    {
        switch (messageType)
        {
            case MessageType.Info:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case MessageType.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case MessageType.Success:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case MessageType.Verbose:
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            default:
                Console.ResetColor();
                break;
        }

        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Info(string message)
    {
        WriteMessage($"[Info] {message}", MessageType.Info);
    }

    public void Warning(string message)
    {
        WriteMessage($"[Warning] {message}", MessageType.Warning);
    }

    public void Error(string message)
    {
        WriteMessage($"[Error] {message}", MessageType.Error);
    }

    public void Success(string message)
    {
        WriteMessage($"[Success] {message}", MessageType.Success);
    }

    public void Verbose(string message)
    {
        if (!_enableVerbose) return;
        WriteMessage($"[Verbose] {message}", MessageType.Verbose);
    }

    public void SetEnableVerbose(bool enabled)
    {
        _enableVerbose = enabled;
    }
}