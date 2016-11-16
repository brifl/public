using System;
using System.Threading.Tasks;

namespace PlantSitter
{
    internal class Pin<T>
    {
        private readonly Func<Task<T>> _provider;

        public Pin(Func<Task<T>> provider)
        {
            _provider = provider;
        }

        public async Task<T> GetAsync()
        {
            return await _provider.Invoke();
        }
    }
}