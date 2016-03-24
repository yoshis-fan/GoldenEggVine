using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public class CLevelSprite : CLevelEntity
	{
		private int _index;

		public CLevelSprite(ArraySegment<byte> sprite) : base()
		{
			_index = sprite.Array[sprite.Offset] + ((sprite.Array[sprite.Offset + 1] & 0x1) == 0 ? 0 : 0x100);
			DeterminePosition(new SVector((byte)sprite.Array[sprite.Offset + 2], (byte)(sprite.Array[sprite.Offset + 1] >> 1)));
			SetType();
		}

		public override int GetSize()
		{
			return 3;
		}

		public override void SetType()
		{
			_entitytype = ELevelEntityType.Sprite;
		}

		public override void DeterminePosition(SVector position)
		{
			_position = position;
		}

        public override byte[] ToByteArray()
        {
            return new byte[]{ (byte)( _index > 0xFF ? _index - 0x100 : _index ), (byte)((_position._y << 1) + (_index > 0xFF ? 1 : 0)), _position._x};
        }

        public override void DrawEntity(System.Drawing.Graphics g, int offset)
        {
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 0, 0)), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), 0x10, 0x10);
            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), 0x10, 0x10);
        }
	}
}
