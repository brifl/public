using System;
using System.Threading.Tasks;
using Pi3BackgroundApp.Common;

namespace Pi3BackgroundApp
{
    internal class TemperatureSensor : ISensor<Temperature>
    {
        public bool IsInitialized { get; set; }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public string Name { get; }

        public Temperature GetValue()
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<Temperature> observer)
        {
            throw new NotImplementedException();
        }
    }
}
