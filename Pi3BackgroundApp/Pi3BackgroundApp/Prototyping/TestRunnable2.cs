using System.Threading.Tasks;
using Pi3BackgroundApp.Common;
using Pi3BackgroundApp.Devices;

namespace Pi3BackgroundApp.Prototyping
{
    internal class TestRunnable2 : IRunnable
    {
        private readonly IPollable<TemperatureHumidity> _tempHumidity;

        public TestRunnable2(IPollable<TemperatureHumidity> tempHumidity)
        {
            _tempHumidity = tempHumidity;
        }

        public Task Run()
        {
            var th = _tempHumidity.GetValue();
            
            return Task.CompletedTask;
        }
    }
}