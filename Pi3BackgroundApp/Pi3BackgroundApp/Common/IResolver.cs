using System.Collections.Generic;

namespace Pi3BackgroundApp.Common
{
    internal interface IResolver
    {
        T Resolve<T>(string name = null);

        IEnumerable<object> ResolveAll();
    }
}
