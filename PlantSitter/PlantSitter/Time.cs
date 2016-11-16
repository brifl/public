using System;
using System.Threading.Tasks;

namespace PlantSitter
{
    internal class Time : ISensor<DateTime>
    {
        public bool IsInitialized { get; set; }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public DateTime GetValue()
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<DateTime> observer)
        {
            throw new NotImplementedException();
        }
    }
}