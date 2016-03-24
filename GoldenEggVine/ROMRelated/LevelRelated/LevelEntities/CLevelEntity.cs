using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public enum ELevelEntityType { Object, Sprite , ScreenExit }

	public struct SVector
	{
		public byte _x;
		public byte _y;

		public SVector(byte x, byte y)
		{
			_x = x;
			_y = y > 0x7F ? (byte)0x7F : y;
		}

		public static SVector operator+(SVector a, SVector b)
		{
			return new SVector((byte)(a._x + b._x), (byte)(a._y + b._y));
		}

		public static SVector operator-(SVector a, SVector b)
		{
			return new SVector((byte)(a._x - b._x), (byte)(a._y - b._y));
		}

		public static bool operator ==(SVector a, SVector b)
		{
			return ((a._x == b._x) && (a._y == b._y));
		}

		public static bool operator !=(SVector a, SVector b)
		{
			return !(a == b);
		}

		public bool InScreenExitBounds()
		{
			return _x < 16 && _y < 8;
		}
	}

	public abstract class CLevelEntity
	{
		protected SVector _position;
		protected ELevelEntityType _entitytype;
		protected bool _exists;

		public CLevelEntity() { _exists = true; }
		public virtual void DeterminePosition(SVector position) { }
		public virtual void SetType() { }
		public virtual void DrawEntity(System.Drawing.Graphics g, int offset) { }

		public void Delete()
		{
			_exists = false;
		}

		public void Create()
		{
			_exists = true;
		}

		public void SetPosition(SVector position)
		{
			_position = position;
		}

		public void SetPosition(byte x, byte y)
		{
			_position = new SVector(x, y);
		}

		public SVector GetPosition()
		{
			return _position;
		}

		public ELevelEntityType GetEntityType()
		{
			return _entitytype;
		}

        public virtual ELevelObjectType GetObjectType()
        {
            return ELevelObjectType.LevelObject;
        }

        public virtual byte[] ToByteArray() { return new byte[]{ }; }

		public bool Exists()
		{
			return _exists;
		}

		public virtual int GetSize() { return -1; }
	}
}