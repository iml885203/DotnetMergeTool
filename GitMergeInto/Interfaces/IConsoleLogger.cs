namespace GitMergeInto.Interfaces;

public interface IConsoleLogger
{
    void Info(string message);
    void Warning(string message);
    void Error(string message);
    void Success(string message);
    void Verbose(string message);
}