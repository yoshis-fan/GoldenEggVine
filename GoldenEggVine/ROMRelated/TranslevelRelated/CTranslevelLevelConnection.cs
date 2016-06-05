using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.TranslevelRelated
{
	public struct SLevelScreen
	{
		public int _levelindex;
		public SVector _position;

		public SLevelScreen(int levelindex, SVector position)
		{
			if(levelindex > -1 && levelindex < CYIROM.NumLevelIndices)
			{
				_levelindex = levelindex;
			}
			else
			{
				throw new Exception("When creating an SLevelScreen, Level Index is not in range 0x00 to 0xDD, it was 0x" + String.Format("{0:X2}",levelindex));
			}
			if(position.InScreenExitBounds())
			{
				_position = position;
			}
			else
			{
				throw new Exception("When creating an SLevelScreen, Screen coordinates are out of bounds");
			}
		}
	}

	public class CTranslevelLevelConnection
	{
		private List<int> _connectedlevels;

		public CTranslevelLevelConnection()
		{

		}
	}
}
