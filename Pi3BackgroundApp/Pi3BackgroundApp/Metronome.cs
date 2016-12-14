using System;
using System.Threading.Tasks;

namespace Pi3BackgroundApp
{
    internal class Metronome : IMetronome
    {
        private const int DelayTimeMs = 500;
        private readonly Observable<long> _secondsFromEpoch = new Observable<long>();
        private readonly IPollable<DateTime> _time;
        private readonly Observable<DateTime> _timeEverySecond = new Observable<DateTime>();
        private bool _isRunning;
        private long _lastSeconds;

        public Metronome(IPollable<DateTime> time)
        {
            _time = time;
        }

        public bool IsInitialized { get; set; }

        public Task Initialize()
        {
            _isRunning = true;
            Task.Factory.StartNew(Run);
            IsInitialized = true;
            return Task.CompletedTask;
        }

        public ISubscribable<long> SecondsFromEpoch => _secondsFromEpoch;

        public ISubscribable<DateTime> TimeEverySecond => _timeEverySecond;

        public void Kill()
        {
            _isRunning = false;
        }

        private void Run()
        {
            while (_isRunning)
            {
                var now = _time.GetValue();
                var ticks = now.Ticks;
                var tickSeconds = ticks / TimeSpan.TicksPerSecond;
                if (tickSeconds != _lastSeconds)
                {
                    Update(now, tickSeconds);
                }
                Task.Delay(DelayTimeMs);
            }
        }

        private void Update(DateTime now, long tickSeconds)
        {
            _lastSeconds = tickSeconds;
            _secondsFromEpoch.Update(_lastSeconds);
            _timeEverySecond.Update(now);
        }
    }
}
