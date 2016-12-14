using System;

namespace Pi3BackgroundApp.Common
{
    internal interface IMetronome : IInitializable
    {
        ISubscribable<long> SecondsFromEpoch { get; }
        ISubscribable<DateTime> TimeEverySecond { get; }
    }
}