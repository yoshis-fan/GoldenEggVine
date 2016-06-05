using GoldenEggVine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.TranslevelRelated
{
	public class CMidwayData
	{
		private int _translevel;
		private ArraySegment<byte>[] _midways = new ArraySegment<byte>[4];

		public CMidwayData(byte[] rombytes, int translevel, int midwaypointers, int midwaydata)
		{
			if (translevel < 0 || translevel >= CYIROM.NumTransLevels)
			{
				throw new MidwayDataAddressPointerException("Translevel Index must be between 0x00 and 0x47, but was specified as " + translevel);
			}
			_translevel = translevel;

			_midways[0] = new ArraySegment<byte>(rombytes, midwaydata + rombytes[midwaypointers + (translevel << 1)] + rombytes[midwaypointers + (translevel << 1) + 1] * 0x100, 4);
			_midways[1] = new ArraySegment<byte>(_midways[0].Array, _midways[0].Offset + 4, 4);
			_midways[2] = new ArraySegment<byte>(_midways[1].Array, _midways[1].Offset + 4, 4);
			_midways[3] = new ArraySegment<byte>(_midways[2].Array, _midways[2].Offset + 4, 4);
		}

		public int GetLevel(int itemmemory)
		{
			if (itemmemory < 0 || itemmemory > 3)
			{
				throw new InvalidItemMemoryIndexException("Item Memory Index must be between 0x0 and 0x3, but was specified as " + itemmemory);
			}
			return _midways[itemmemory].Array[_midways[itemmemory].Offset + 0];
		}

		public int GetX(int itemmemory)
		{
			if (itemmemory < 0 || itemmemory > 3)
			{
				throw new InvalidItemMemoryIndexException("Item Memory Index must be between 0x0 and 0x3, but was specified as " + itemmemory);
			}
			return _midways[itemmemory].Array[_midways[itemmemory].Offset + 1];
		}

		public int GetY(int itemmemory)
		{
			if (itemmemory < 0 || itemmemory > 3)
			{
				throw new InvalidItemMemoryIndexException("Item Memory Index must be between 0x0 and 0x3, but was specified as " + itemmemory);
			}
			return _midways[itemmemory].Array[_midways[itemmemory].Offset + 2];
		}

		public int GetEntranceType(int itemmemory)
		{
			if (itemmemory < 0 || itemmemory > 3)
			{
				throw new InvalidItemMemoryIndexException("Item Memory Index must be between 0x0 and 0x3, but was specified as " + itemmemory);
			}
			return _midways[itemmemory].Array[_midways[itemmemory].Offset + 3];
		}
	}
}
