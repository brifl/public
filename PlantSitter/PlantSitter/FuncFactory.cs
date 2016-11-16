using System;

namespace PlantSitter
{
    internal class FuncFactory<T> : IFactory<T>
    {
        private readonly Func<T> _source;

        public FuncFactory(Func<T> source)
        {
            _source = source;
        }

        public T Build()
        {
            return _source.Invoke();
        }
    }
}