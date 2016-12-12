using System.Collections.Generic;
using System.Threading.Tasks;
using Pi3BackgroundApp.Prototyping;

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
            _dependencies.Register(r => new TestRunnable2(r.Resolve<ArduinoI2C>(Instances.ArduinoTempHumidity)), true);
            _dependencies.Register(r => new ArduinoI2C(Instances.ArduinoTempHumidity, Pi3.I2C_0x40), true, Instances.ArduinoTempHumidity);
        }

        private static class Instances
        {
            public const string ArduinoTempHumidity = "ArduinoTempHumidity";
        }
    }
}
