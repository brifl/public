using System;

namespace Pi3BackgroundApp.Common
{
    internal class Time : IPollable<DateTime>
    {
        public DateTime GetValue()
        {
            return DateTime.UtcNow;
        }
    }
}
