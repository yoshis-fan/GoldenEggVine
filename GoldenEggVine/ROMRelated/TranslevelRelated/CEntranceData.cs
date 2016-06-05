using GoldenEggVine.Exceptions;
using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.TranslevelRelated
{
	public class CEntranceData
	{
		private int _translevel;
		private ArraySegment<byte> _entrance;

		public int TransLevel => _translevel;

		public CEntranceData(byte[] rombytes, int translevel, int entrancepointers, int entrancedata)
		{
			if (translevel < 0 || translevel >= CYIROM.NumTransLevels)
			{
				throw new InvalidTranslevelIndexException("Translevel Index must be between 0x00 and 0x47, but was specified as " + translevel);
			}
			_translevel = translevel;

			_entrance = new ArraySegment<byte>(rombytes, entrancedata + rombytes[entrancepointers + (translevel << 1)] + rombytes[entrancepointers + (translevel << 1) + 1] * 0x100, 4);
		}

		public int GetLevel()
		{
			return _entrance.Array[_entrance.Offset + 0];
		}

		public int GetX()
		{
			return _entrance.Array[_entrance.Offset + 1];
		}

		public int GetY()
		{
			return _entrance.Array[_entrance.Offset + 2];
		}

		public int GetUnlock()
		{
			return _entrance.Array[_entrance.Offset + 3];
		}


		public byte[] GetData()
		{
			byte[] target = new byte[4];
			Array.Copy(_entrance.Array, _entrance.Offset, target, 0, 4);
			return target;
		}
		public void SetData(byte level, byte x, byte y, byte unlock)
		{
			_entrance.Array[_entrance.Offset + 0] = level;
			_entrance.Array[_entrance.Offset + 1] = x;
			_entrance.Array[_entrance.Offset + 2] = y;
			_entrance.Array[_entrance.Offset + 3] = unlock;
		}

		public HashSet<int> GetAllReachableLevels(CYIROM rom)
		{
			var result = new HashSet<int>();
			getAllReachableLevels(rom, GetLevel(), result);
			return result;
		}
		private void getAllReachableLevels(CYIROM rom, int level, HashSet<int> traversedLevels)
		{
			if (traversedLevels.Contains(level))
				return;

			traversedLevels.Add(level);

			var levelData = rom.GetLevelData(level);
			foreach (CLevelScreenExit exit in levelData.ScreenExits())
				getAllReachableLevels(rom, exit.GetDestinationLevel(), traversedLevels);
		}
	}
}
