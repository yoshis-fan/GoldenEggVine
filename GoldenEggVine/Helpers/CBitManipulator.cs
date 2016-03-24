using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Helpers
{
	public static class CBitManipulator
	{
		public static int GetValue(byte[] b, int start, int length)
		{
			//Validate Maxlength
			if (length > 31)
			{
				throw new Exception("Value cannot be stored entirely in the return-value. The maximum length is 31, you specified " + length);
			}
			//Validate Consistency
			if (start + length > (b.Length << 3))
			{
				throw new Exception("A/Some bit/s is/are out of bounds. Last Index of your specified range is " + (start + length - 1) + ", the Byte-Array's last Bit-Index is " + ((b.Length << 3) - 1));
			}
			int bitsum = 0;
			for (int i = 0; i < length; i++)
			{
				bitsum += GetBit(b, start + i) ? (1 << (length - i - 1)) : 0;
			}
			return bitsum;
		}

		public static void SetValue(byte[] b, int value, int start, int length)
		{
			//Validate Maxlength
			if (length > 31)
			{
				throw new Exception("Value cannot be stored entirely in the return-value. The maximum length is 31, you specified " + length);
			}
			//Validate Consistency
			if (start + length >= (b.Length << 3))
			{
				throw new Exception("A/Some bit/s is/are out of bounds. Last Index of your specified range is " + (start + length - 1) + ", the Byte-Array's last Bit-Index is " + ((b.Length << 3) - 1));
			}
			for (int i = 0; i < length; i++)
			{
				SetBit(b, start + i, (((value >> (length - i - 1))) & 1) == 1);
			}
		}

		private static bool GetBit(byte[] b, int position)
		{
			return ((b[position >> 3] >> (7 - (position & 7))) & 1) == 1;
		}

		private static void SetBit(byte[] b, int position, bool bit)
		{
			if(bit)
			{
				b[position >> 3] = (byte)((b[position >> 3] & (0xFF - (1 << (7 - position & 7)))) + (1 << (7 - (position & 7))));
			}
			else
			{
				b[position >> 3] = (byte)(b[position >> 3] & (0xFF - (1 << (7 - position & 7))));
			}
		}
	}
}
