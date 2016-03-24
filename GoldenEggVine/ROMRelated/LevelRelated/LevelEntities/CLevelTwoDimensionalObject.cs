using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	class CLevelTwoDimensionalObject : CLevelObject
	{
		private SSize _internalsize;

		public CLevelTwoDimensionalObject(ArraySegment<byte> leveltwodimensionalobject) : base(leveltwodimensionalobject)
		{
			_objecttype = ELevelObjectType.TwoDimensional;
			_internalsize = new SSize((sbyte)leveltwodimensionalobject.Array[leveltwodimensionalobject.Offset + 3], (sbyte)leveltwodimensionalobject.Array[leveltwodimensionalobject.Offset + 4]);
		}

		public override int GetSize()
		{
			return 5;
		}

		public override void SetType()
		{
			_entitytype = ELevelEntityType.Object;
		}

        public override byte[] ToByteArray()
        {
            return new byte[] { _index, (byte)((_position._y & 0xF0) + ((_position._x & 0xF0) >> 4)), (byte)(((_position._y & 0x0F) << 4) + (_position._x & 0x0F)), (byte)_internalsize._x, (byte)_internalsize._y };
        }

		public override void DrawEntity(System.Drawing.Graphics g, int offset)
		{
            if (_internalsize._x >= 0 && _internalsize._y >= 0)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 255, 255)), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internalsize._x + 1) << 4), ((_internalsize._y + 1) << 4));
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), (_position._x << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internalsize._x + 1) << 4), ((_internalsize._y + 1) << 4));
            }
            else if (_internalsize._x >= 0 && _internalsize._y < 0)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 255, 128)), (_position._x << 4), ((_position._y - 1) << 4) + (offset << 4), ((_internalsize._x + 1) << 4), ((_internalsize._y * -1 + 1) << 4));
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), (_position._x << 4), ((_position._y - 1) << 4) + (offset << 4), ((_internalsize._x + 1) << 4), ((_internalsize._y * -1 + 1) << 4));
            }
            else if (_internalsize._x < 0 && _internalsize._y >= 0)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 128, 255)), ((_position._x - 1) << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internalsize._x * -1 + 1) << 4), ((_internalsize._y + 1) << 4));
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), ((_position._x - 1) << 4) + (offset << 4), (_position._y << 4) + (offset << 4), ((_internalsize._x * -1 + 1) << 4), ((_internalsize._y + 1) << 4));
            }
            else
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 128, 128)), ((_position._x - 1) << 4) + (offset << 4), ((_position._y - 1) << 4) + (offset << 4), ((_internalsize._x * -1 + 1) << 4), ((_internalsize._y * -1 + 1) << 4));
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 0, 0), 1), ((_position._x - 1) << 4) + (offset << 4), ((_position._y - 1) << 4) + (offset << 4), ((_internalsize._x * -1 + 1) << 4), ((_internalsize._y * -1 + 1) << 4));
            }
		}
	}
}