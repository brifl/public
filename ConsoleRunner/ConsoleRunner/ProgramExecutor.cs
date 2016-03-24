namespace ConsoleRunner
{
    public class ProgramExecutor
    {
        private static readonly IConsoleOut Console = new ConsoleOut();
        protected static void Execute(string operationType, string[] args)
        {
            var factory = OperationFactory.Create();
            var operation = factory.GetOperation(operationType);
            operation.Execute(Console, args);
        }
    }
}