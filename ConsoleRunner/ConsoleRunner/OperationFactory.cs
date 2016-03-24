using System;
using System.Collections.Generic;

namespace ConsoleRunner
{
    public class OperationFactory : IOperationFactory
    {
        private static readonly Dictionary<string, Func<IOperation>> Registry = new Dictionary<string, Func<IOperation>>(StringComparer.InvariantCultureIgnoreCase);

        public static void Register(string operationType, Func<IOperation> factory)
        {
            Registry[operationType] = factory;
        }

        public static IOperationFactory Create()
        {
            return new OperationFactory();
        }

        public IOperation GetOperation(string operationType)
        {
            if (!Registry.ContainsKey(operationType))
            {
                throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null);
            }

            return Registry[operationType].Invoke();
        }
    }
}