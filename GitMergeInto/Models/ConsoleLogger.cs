using GitMergeInto.Enums;
using GitMergeInto.Interfaces;

namespace GitMergeInto.Models;

public class ConsoleLogger : IConsoleLogger
{
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
}