using System;

namespace PlantSitter
{
    internal class Time : IPollable<DateTime>
    {
        public DateTime GetValue()
        {
            return DateTime.UtcNow;
        }
    }
}
