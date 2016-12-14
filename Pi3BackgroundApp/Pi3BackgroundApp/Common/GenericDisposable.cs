using System;

namespace Pi3BackgroundApp.Common
{
    internal class GenericDisposable : IDisposable
    {
        private Action _onDispose;

        public GenericDisposable(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            try
            {
                _onDispose?.Invoke();
            }
            finally
            {
                _onDispose = null;
            }
        }
    }
}