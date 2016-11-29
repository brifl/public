using System.Collections.Generic;

namespace Pi3BackgroundApp
{
    internal interface IResolver
    {
        T Resolve<T>(string name = null);

        IEnumerable<object> ResolveAll();
    }
}
