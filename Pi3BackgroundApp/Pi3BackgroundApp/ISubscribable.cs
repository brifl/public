using System;

namespace Pi3BackgroundApp
{
    internal interface ISubscribable<out T>
    {
        IDisposable Subscribe(Action<T> subscriber);
    }
}