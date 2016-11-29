using System;

namespace Pi3BackgroundApp
{
    internal class Time : IPollable<DateTime>
    {
        public DateTime GetValue()
        {
            return DateTime.UtcNow;
        }
    }
}
