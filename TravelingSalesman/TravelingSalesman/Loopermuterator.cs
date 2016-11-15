using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesman
{
    internal class Loopermuterator<T> : IEnumerator<T[]>
    {
        private readonly List<T[]> _permutations = new List<T[]>();
        private int _index;
        private T[] _original;

        public Loopermuterator(IEnumerable<T> source)
        {
            _original = source.ToArray();
            Initialize();
        }

        public void Dispose()
        {
            Current = null;
            _original = null;
        }

        public bool MoveNext()
        {
            if (_index < _permutations.Count)
            {
                Current = _permutations[_index++];
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _index = 0;
        }

        public T[] Current { get; private set; }

        object IEnumerator.Current => Current;

        private void Initialize()
        {
            var subset = new T[_original.Length - 1];
            for (int i = 1; i < _original.Length; i++)
            {
                subset[i - 1] = _original[i];
            }
            GenerateSwapPermutaion(subset.Length, subset);
        }

        private void GenerateSwapPermutaion(int nth, T[] swapArray)
        {
            while (true)
            {
                if (nth <= 1)
                {
                    _permutations.Add(Permutation(_original[0], swapArray));
                    return;
                }
                for (var i = 0; i < nth - 1; i++)
                {
                    GenerateSwapPermutaion(nth - 1, swapArray);
                    Swap(nth%2 == 0 ? i : 0, nth - 1, swapArray);
                }
                nth = nth - 1;
            }
        }

        private T[] Permutation(T first, T[] swapArray)
        {
            var copy = new T[swapArray.Length + 1];
            copy[0] = first;
            for (int i = 1; i < copy.Length; i++)
            {
                copy[i] = swapArray[i - 1];
            }
            return copy;
        }

        private static void Swap(int a, int b, T[] swapArray)
        {
            var temp = swapArray[a];
            swapArray[a] = swapArray[b];
            swapArray[b] = temp;
        }
    }
}