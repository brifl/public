using System;

namespace Pi3BackgroundApp.Common
{
    internal interface ISchedule : IDisposable
    {
        void RunWhenDue(Action action);
    }
}