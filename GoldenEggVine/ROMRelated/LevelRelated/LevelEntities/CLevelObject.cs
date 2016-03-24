using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public enum ELevelObjectType { LevelObject, OneDimensional, TwoDimensional, Extended };

	public struct SSize
	{
		public sbyte _x;
		public sbyte _y;

		public SSize(sbyte x, sbyte y)
		{
			_x = x;
			_y = y;
		}
	}

	public abstract class CLevelObject : CLevelEntity
	{
		protected byte _index;
		protected ELevelObjectType _objecttype;

		public CLevelObject(ArraySegment<byte> root) : base()
		{
			_objecttype = ELevelObjectType.LevelObject;
			SetIndex(root);
            DeterminePosition(new SVector((byte)(((root.Array[root.Offset + 1] & 0x0F) << 4)+(root.Array[root.Offset + 2] & 0x0F)), (byte)((root.Array[root.Offset + 1] & 0xF0) + ((root.Array[root.Offset + 2] & 0xF0) >> 4))));
		}

        public override ELevelObjectType GetObjectType()
        {
 	        return _objecttype;
        }

        public override void DeterminePosition(SVector position)
        {
            _position = position;
        }

		public virtual void SetIndex(ArraySegment<byte> root)
		{
			_index = root.Array[root.Offset];
		}

		public static CLevelObject CreateObject(ELevelObjectType type, ArraySegment<byte> obj)
		{
			switch (type)
			{
				case ELevelObjectType.OneDimensional:
					return new CLevelOneDimensionalObject(obj);
				case ELevelObjectType.TwoDimensional:
					return new CLevelTwoDimensionalObject(obj);
				case ELevelObjectType.Extended:
					return new CLevelExtendedObject(obj);
				default:
					return null;
			}
		}
	}
}