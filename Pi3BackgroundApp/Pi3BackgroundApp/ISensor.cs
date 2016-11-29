using System;

namespace Pi3BackgroundApp
{
    internal interface ISensor<out TValue> : IPollable<TValue>, IObservable<TValue>
    {
    }
}
