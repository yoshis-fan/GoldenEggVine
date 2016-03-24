using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Helpers
{
	static class CSubArray
	{
		public static T[] StartLength<T>(T[] array, int start, int length)
		{
			T[] sub = new T[length];
			for (int i = start, j = 0; i < length; i++, j++)
			{
				sub[j] = array[i];
			}
			return sub;
		}

		public static T[] FromTo<T>(T[] array, int start, int end)
		{
			return StartLength(array, start, end - start + 1);
		}
	}
}
