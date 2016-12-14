using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pi3BackgroundApp.Common
{
    internal class Initializer : IInitializable
    {
        private readonly IEnumerable<IInitializable> _initializables;

        public Initializer(IEnumerable<IInitializable> initializables)
        {
            _initializables = initializables;
        }

        public bool IsInitialized { get; set; }

        public async Task Initialize()
        {
            if (!IsInitialized)
            {
                foreach (var initializable in _initializables)
                {
                    if (!initializable.IsInitialized)
                    {
                        await initializable.Initialize();
                        initializable.IsInitialized = true;
                    }
                }
            }
        }
    }
}
