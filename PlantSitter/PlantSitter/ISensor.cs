using System;

namespace PlantSitter
{
    internal interface ISensor<out TValue> : IPollable<TValue>, IObservable<TValue>
    {
    }
}
