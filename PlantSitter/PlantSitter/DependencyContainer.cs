using System;
using System.Collections.Generic;
using System.Linq;

namespace PlantSitter
{
    internal class DependencyContainer : IRegistry, IResolver
    {
        private readonly Dictionary<string, Func<IResolver, object>> _constructors =
            new Dictionary<string, Func<IResolver, object>>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, object> _instances =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public void Register<T>(Func<IResolver, T> construction, bool cacheInstance = false, string name = null)
        {
            name = name ?? typeof(T).Name;
            Func<IResolver, object> boxedConstruction = r => construction.Invoke(r);
            var func = cacheInstance ? r => ResolveCached(name, () => boxedConstruction(r)) : boxedConstruction;
            _constructors[name] = func;
        }

        public T Resolve<T>(string name)
        {
            name = name ?? typeof(T).Name;
            var instance = _constructors[name].Invoke(this);
            return (T) instance;
        }

        public IEnumerable<object> ResolveAll()
        {
            var all = new List<object>(_constructors.Count);
            all.AddRange(_constructors.Keys.Select(Resolve<object>));
            return all;
        }

        private object ResolveCached(string name, Func<object> construction)
        {
            if (!_instances.ContainsKey(name))
            {
                _instances[name] = construction.Invoke();
            }
            return _instances[name];
        }
    }
}
