using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public class CLevelExtendedObject : CLevelObject
	{
		public CLevelExtendedObject(ArraySegment<byte> extendedobject) : base(extendedobject)
		{
			_objecttype = ELevelObjectType.Extended;
		}

		public override void SetIndex(ArraySegment<byte> root)
		{
			_index = root.Array[root.Offset + 3];
		}

		public override int GetSize()
		{
			return 4;
		}

		public override void SetType()
		{
			_entitytype = ELevelEntityType.Object;
		}

        public override byte[] ToByteArray()
        {
            return new byte[] { 0x00, (byte)((_position._y & 0xF0) + ((_position._x & 0xF0) >> 4)), (byte)(((_position._y & 0x0F) << 4) + (_position._x & 0x0F)), _index };
        }

		public override void DrawEntity(System.Drawing.Graphics g, int offset)
		{
			g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(64,64,64)), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), 0x10, 0x10);
		}
	}
}