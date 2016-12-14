using System;

namespace Pi3BackgroundApp
{
    internal interface ISchedule : IDisposable
    {
        void RunWhenDue(Action action);
    }
}