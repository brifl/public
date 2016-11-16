using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSitter
{
    internal class InitializedRunnable : IRunnable
    {
        private readonly IRunnable _runnable;
        private readonly IInitializable _initializer;

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