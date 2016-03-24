using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Helpers
{
    public class CByteStream
    {
        private byte[] _bytes;
        private int _size;


        public CByteStream()
        {
            _bytes = new byte[16];
            _size = 0;
        }

        public void Append(byte b)
        {
            if(_size + 1 > _bytes.Length)
            {
                byte[] replace = new byte[_bytes.Length << 1];
                Array.Copy(_bytes, replace, _bytes.Length);
                _bytes = replace;
            }
            _bytes[_size] = b;
            ++_size;
        }

        public void Append(byte[] b)
        {
            if (_size + b.Length > _bytes.Length)
            {
                byte[] replace;
                do
                {
                    replace = new byte[_bytes.Length << 1];
                }
                while (_size + b.Length > replace.Length);
                Array.Copy(_bytes, replace, _bytes.Length);
                _bytes = replace;
            }
            Array.Copy(b, 0, _bytes, _size, b.Length);
            _size += b.Length;
        }

        public byte[] ToByteArray()
        {
            byte[] content = new byte[_size];
            Array.Copy(_bytes, content, _size);
            return content;
        }

    }
}
