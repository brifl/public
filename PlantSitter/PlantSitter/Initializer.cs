using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace PlantSitter
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