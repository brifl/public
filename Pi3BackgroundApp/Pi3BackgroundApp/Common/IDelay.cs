using System;

namespace Pi3BackgroundApp.Common
{
    internal interface IDelay
    {
        void Milliseconds(int ms);
        void Microseconds(int mus);
        void Seconds(int seconds);
        MinWaiter GetWaiter(TimeSpan minWaitTime);
    }
}