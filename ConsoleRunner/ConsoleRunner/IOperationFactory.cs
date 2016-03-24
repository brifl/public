namespace ConsoleRunner
{
    public interface IOperationFactory
    {
        IOperation GetOperation(string operationType);
    }
}