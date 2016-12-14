using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pi3BackgroundApp.Common
{
    internal class AggregateDisposable : IDisposable
    {
        private IDisposable[] _disposables;
        public AggregateDisposable(IEnumerable<object> instances)
        {
            _disposables = instances.OfType<IDisposable>().ToArray();
        }

        public void Dispose()
        {
            if (_disposables == null)
            {
                return;
            }

            foreach (var disposable in _disposables)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Failed to dispose type {e.GetType()}");
                }
            }

            _disposables = null;
        }
    }
}