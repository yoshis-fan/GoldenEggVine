using GoldenEggVine.EditorRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public enum EEntranceType { Door=0, Skis=1, PipeRight=2, PipeLeft=3, PipeDown=4, PipeUp=5, EntranceRight=6, EntranceLeft=7, EntranceDown=8, EntranceUp=9, Moon=10 };

	public class CLevelLevelScreenExit : CLevelScreenExit
	{
		private byte _level;
		private EEntranceType _entrancetype;

		public CLevelLevelScreenExit(ArraySegment<byte> screenexit) : base(screenexit)
		{
			if(screenexit.Array[screenexit.Offset + 1] < CYIROM.NumLevelIndices)
			{
				_level = screenexit.Array[screenexit.Offset + 1];
			}
			else
			{
				throw new Exception("Index 0x" + String.Format("{0:X2}",screenexit.Array[screenexit.Offset + 1]) + " is not a valid Level Index");
			}
			if(Enum.IsDefined(typeof(EEntranceType), (int)screenexit.Array[screenexit.Offset + 4]))
			{
				_entrancetype = ((EEntranceType)((int)screenexit.Array[screenexit.Offset + 4]));
			}
			else
			{
				throw new Exception("Index 0x" + String.Format("{0:X2}", screenexit.Array[screenexit.Offset + 4]) + " is not a valid Entrance Type");
			}
		}

		public CLevelLevelScreenExit(SVector position, byte level, byte positionx, byte positiony, EEntranceType entrancetype) : base(position, positionx, positiony)
		{
			_level = level;
			_entrancetype = entrancetype;
		}

		public override int GetDestinationLevel()
		{
			return _level;
		}

		public EEntranceType GetEntranceType()
		{
			return _entrancetype;
		}

		public override byte[] ToByteArray()
		{
			return new byte[] { (byte)(_position._x + (_position._y << 4)), _level, _positionx, _positiony, (byte)_entrancetype };
		}

		public override void DrawEntity(System.Drawing.Graphics g, int offset)
		{
			g.DrawRectangle(CGlobalDrawing._redouterline, (_position._x << 8) + (offset << 4), (_position._y << 8) + (offset << 4), 0xFF, 0xFF);
			g.DrawRectangle(CGlobalDrawing._redinnerline, (_position._x << 8) + 0x2 + (offset << 4), (_position._y << 8) + 0x2 + (offset << 4), 0xFC, 0xFC);

			g.FillRectangle(CGlobalDrawing._datarect, (_position._x << 8) + 3 + (offset << 4), (_position._y << 8) + 3 + (offset << 4), 0xFA, 0xD);
			g.DrawString(String.Format("{0:X2}", (_position._y << 4) + _position._x) + "      L: " + String.Format("{0:X2}", _level) + "   X: " + String.Format("{0:X2}", _positionx) + "   Y: " + String.Format("{0:X2}", _positiony) + "   E: " + String.Format("{0:X2}", (int)_entrancetype), CGlobalDrawing._font, CGlobalDrawing._fontbrush, new System.Drawing.Point((_position._x << 8) + 2 + (offset << 4), (_position._y << 8) + 3 + (offset << 4)));
		}
	}
}
