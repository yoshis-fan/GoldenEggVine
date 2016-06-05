using GoldenEggVine.EditorRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public enum EMinibattle { ThrowingBalloons4=0xDE, ThrowingBalloons5=0xDF, ThrowingBalloons6=0xE0, GameFreeze1=0xE1, GatherCoins=0xE2, PoppingBalloons=0xE3, PoppingBalloonsMovingPlatforms=0xE4, GameFreeze2=0xE5, GameFreeze3=0xE6, WatermelonSeedSpittingContest=0xE7 };

	public class CLevelMinibattleScreenExit : CLevelScreenExit
	{
		private EMinibattle _minibattle;
		private byte _returnlevel;

		public CLevelMinibattleScreenExit(ArraySegment<byte> screenexit) : base(screenexit)
		{
			_minibattle = ((EMinibattle)((int)screenexit.Array[screenexit.Offset + 1]));
			_returnlevel = screenexit.Array[screenexit.Offset + 4];
		}

		public CLevelMinibattleScreenExit(SVector position, EMinibattle minibattle, byte returnlevel, byte positionx, byte positiony) : base(position, positionx, positiony)
		{
			_minibattle = minibattle;
			_returnlevel = returnlevel;
		}

		public override int GetDestinationLevel()
		{
			return _returnlevel;
		}

		public EMinibattle GetMinibattle()
		{
			return _minibattle;
		}

		public override byte[] ToByteArray()
		{
			return new byte[] { (byte)(_position._x + (_position._y << 4)), (byte)_minibattle, _positionx, _positiony, _returnlevel };
		}

		public override void DrawEntity(System.Drawing.Graphics g, int offset)
		{
			g.DrawRectangle(CGlobalDrawing._redouterline, (_position._x << 8) + (offset << 4), (_position._y << 8) + (offset << 4), 0xFF, 0xFF);
			g.DrawRectangle(CGlobalDrawing._redinnerline, (_position._x << 8) + 0x2 + (offset << 4), (_position._y << 8) + 0x2 + (offset << 4), 0xFC, 0xFC);

			g.FillRectangle(CGlobalDrawing._datarect, (_position._x << 8) + 3 + (offset << 4), (_position._y << 8) + 3 + (offset << 4), 0xFA, 0xD);
			g.DrawString("Minibattle: " + String.Format("{0:X1}", (int)(_minibattle - CYIROM.NumLevelIndices)) + "   X: " + String.Format("{0:X2}", _positionx) + "   Y: " + String.Format("{0:X2}", _positiony) + "   L: " + String.Format("{0:X2}", _returnlevel), CGlobalDrawing._font, CGlobalDrawing._fontbrush, new System.Drawing.Point((_position._x << 8) + 2 + (offset << 4), (_position._y << 8) + 3 + (offset << 4)));
		}
	}
}
