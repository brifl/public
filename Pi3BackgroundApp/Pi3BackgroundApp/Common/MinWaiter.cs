using System;

namespace Pi3BackgroundApp.Common
{
    internal class MinWaiter
    {
        private readonly double _minTimeMs;
        private readonly IPollable<DateTime> _timeProvider;
        private readonly IDelay _delay;
        private DateTime _lastSet;

        public MinWaiter(TimeSpan minTime, IPollable<DateTime> timeProvider, IDelay delay)
        {
            _minTimeMs = minTime.TotalMilliseconds;
            _timeProvider = timeProvider;
            _delay = delay;
            _lastSet = _timeProvider.GetValue();
        }

        public void Reset()
        {
            _lastSet = _timeProvider.GetValue();
        }

        public void Wait()
        {
            var now = _timeProvider.GetValue();
            var diff = now.Subtract(_lastSet).TotalMilliseconds;
            if (diff < _minTimeMs)
            {
                var waitTime = Convert.ToInt32(_minTimeMs - diff);
                _delay.Milliseconds(waitTime);
            }
        } 
    }
}