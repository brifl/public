using System;
using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal class Delayer : IDelay
    {
        private readonly IPollable<DateTime> _timeProvider;

        public Delayer(IPollable<DateTime> timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void Milliseconds(int ms)
        {
            Task.Delay(ms).Wait();
        }

        public void Microseconds(int mus)
        {
            var nowTicks = _timeProvider.GetValue().Ticks;
            var targetTicks = nowTicks + (mus * 10);
            while (_timeProvider.GetValue().Ticks < targetTicks)
            {
                //cycle in lieu of a microsecond wait
                //not super precise without a realtime OS
            }
        }

        public void Seconds(int seconds)
        {
            Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
        }

        public MinWaiter GetWaiter(TimeSpan minWaitTime)
        {
            return new MinWaiter(minWaitTime, _timeProvider, this);
        }
    }
}