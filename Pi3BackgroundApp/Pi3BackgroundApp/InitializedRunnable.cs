using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal class InitializedRunnable : IRunnable
    {
        private readonly IInitializable _initializer;
        private readonly IRunnable _runnable;

        public InitializedRunnable(IEnumerable<object> instances)
        {
            _runnable = new OrderedRunner(instances.OfType<IRunnable>().ToList());
            _initializer = new Initializer(instances.OfType<IInitializable>().ToList());
        }

        public async Task Run()
        {
            await _initializer.Initialize();
            await _runnable.Run();
        }
    }
}
