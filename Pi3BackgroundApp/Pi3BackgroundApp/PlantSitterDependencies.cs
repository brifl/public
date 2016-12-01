using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal class PlantSitterDependencies : IInitializable, IResolver
    {
        private readonly DependencyContainer _dependencies = new DependencyContainer();

        public bool IsInitialized { get; set; }

        public Task Initialize()
        {
            if (!IsInitialized)
            {
                RegisterAll();
                IsInitialized = true;
            }

            return TaskUtil.Empty;
        }

        public T Resolve<T>(string name = null)
        {
            return _dependencies.Resolve<T>(name);
        }

        public IEnumerable<object> ResolveAll()
        {
            return _dependencies.ResolveAll();
        }

        private void RegisterAll()
        {
            _dependencies.Register(r => new LedRgb(Pi3.Gpio2, Pi3.Gpio3, Pi3.Gpio4), true);
            _dependencies.Register(r => new TestRunnable(r.Resolve<LedRgb>()), true);
        }
    }
}
