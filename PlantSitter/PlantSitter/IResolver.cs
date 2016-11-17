using System.Collections.Generic;

namespace PlantSitter
{
    internal interface IResolver
    {
        T Resolve<T>(string name = null);

        IEnumerable<object> ResolveAll();
    }
}
