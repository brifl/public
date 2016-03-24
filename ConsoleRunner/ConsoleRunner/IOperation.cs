namespace ConsoleRunner
{
    public interface IOperation
    {
        void Execute(IConsoleOut console, string[] args);
    }
}