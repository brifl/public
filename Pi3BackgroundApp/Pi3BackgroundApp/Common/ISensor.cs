using System;

namespace Pi3BackgroundApp.Common
{
    internal interface ISensor<out TValue> : IPollable<TValue>, IObservable<TValue>
    {
    }
}
