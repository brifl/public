using System;

namespace Pi3BackgroundApp.Common
{
    internal interface ISubscribable<out T>
    {
        IDisposable Subscribe(Action<T> subscriber);
    }
}