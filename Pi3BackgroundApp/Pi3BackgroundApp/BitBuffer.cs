using System;
using System.Collections.Generic;

namespace Pi3BackgroundApp
{
    internal class BitBuffer
    {
        private readonly List<bool> _bits;
       
        public BitBuffer(int size)
        {
            if (size % 8 != 0)
            {
                throw new ArgumentException("BitBuffer size must be divisible by 8 for byte conversion");
            }

            _bits = new List<bool>(size);
        }

        public void Add(bool bit)
        {
            _bits.Add(bit);
        }

        public byte[] GetBytes()
        { 
            var bytes = new byte[_bits.Count / 8];

            var chars = new char[8];

            for (int i = 0; i < _bits.Count; i++)
            {
                var charIndex = i % 8;
                chars[charIndex] = _bits[i] ? '1' : '0';

                if (charIndex == 7)
                {
                    var b = Convert.ToByte(new string(chars), 2);
                    bytes[i / 8] = b;
                }
            }
            
            return bytes;
        }
    }
}