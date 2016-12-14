using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pi3BackgroundApp.Common
{
    internal class Observable<T> : ISubscribable<T>
    {
        private readonly ConcurrentDictionary<Action<T>, bool> _subscribers =
            new ConcurrentDictionary<Action<T>, bool>();

        public IDisposable Subscribe(Action<T> subscriber)
        {
            _subscribers[subscriber] = false;

            return new GenericDisposable(() =>
            {
                bool removed;
                _subscribers.TryRemove(subscriber, out removed);
            });
        }

        public void Update(T newValue)
        {
            List<Exception> exceptions = null;
            foreach (var subscriber in _subscribers.Keys)
            {
                try
                {
                    if (!_subscribers[subscriber])
                    {
                        _subscribers[subscriber] = true;
                        Task.Factory.StartNew(() => subscriber.Invoke(newValue))
                            .ContinueWith(t => _subscribers[subscriber] = false);
                    }
                    subscriber.Invoke(newValue);
                }
                catch(Exception e)
                {
                    _subscribers[subscriber] = false;
                    if (exceptions == null)
                    {
                        exceptions = new List<Exception>();
                    }

                    exceptions.Add(e);
                }
            }

            if (exceptions != null)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}