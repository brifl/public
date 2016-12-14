using System;

namespace Pi3BackgroundApp.Common
{
    internal interface IRegistry
    {
        void Register<T>(Func<IResolver, T> construction, bool cacheInstance = false, string name = null);
    }
}
