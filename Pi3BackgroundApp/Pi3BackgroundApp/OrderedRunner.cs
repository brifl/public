using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal class OrderedRunner : IRunnable
    {
        private readonly IEnumerable<IRunnable> _runnables;

        public OrderedRunner(IEnumerable<IRunnable> runnables)
        {
            _runnables = runnables;
        }

        public async Task Run()
        {
            foreach (var runnable in _runnables)
            {
                await runnable.Run();
            }
        }
    }
}
