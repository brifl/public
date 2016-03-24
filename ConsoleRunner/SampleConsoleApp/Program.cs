using ConsoleRunner;

namespace SampleConsoleApp
{
    class Program : ProgramExecutor
    {
        static Program()
        {
            OperationFactory.Register("Echo", () => new EchoOperaion());
        }

        static void Main(string[] args)
        {
            Execute("Echo", args);
        }
    }
}
