using System.Collections;
using System.Collections.Generic;

namespace TravelingSalesman
{
    internal class TspPermuationWalker<T> : IEnumerable<T[]>
    {
        private readonly IEnumerator<T[]> _enumerator;

        public TspPermuationWalker(IEnumerable<T> source)
        {
            _enumerator = new Loopermuterator<T>(source);
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}