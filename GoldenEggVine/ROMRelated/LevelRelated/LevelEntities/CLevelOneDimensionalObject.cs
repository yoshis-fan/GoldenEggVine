using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	class CLevelOneDimensionalObject : CLevelObject
	{
		private sbyte _internallength;

		public CLevelOneDimensionalObject(ArraySegment<byte> levelonedimensionalobject) : base(levelonedimensionalobject)
		{
			_objecttype = ELevelObjectType.OneDimensional;
			_internallength = (sbyte)levelonedimensionalobject.Array[levelonedimensionalobject.Offset + 3];
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
            return new byte[] { _index, (byte)((_position._y & 0xF0) + ((_position._x & 0xF0) >> 4)), (byte)(((_position._y & 0x0F) << 4) + (_position._x & 0x0F)), (byte)_internallength };
        }

		public override void DrawEntity(System.Drawing.Graphics g, int offset)
		{
            if (_internallength < 0)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 255, 0)), ((_position._x + _internallength) << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internallength * -1 + 1) << 4), 0x10);
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), ((_position._x + _internallength) << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internallength * -1 + 1) << 4), 0x10);
            }
            else
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 128, 0)), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internallength + 1) << 4), 0x10);
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internallength + 1) << 4), 0x10);
            }
		}
	}
}
