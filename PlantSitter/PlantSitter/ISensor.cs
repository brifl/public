using System;

namespace PlantSitter
{
    internal interface ISensor<out TValue> : IDevice, IPollable<TValue>, IObservable<TValue>
    {
    }
}