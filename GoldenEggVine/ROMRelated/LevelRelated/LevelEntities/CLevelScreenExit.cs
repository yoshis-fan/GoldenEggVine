using GoldenEggVine.EditorRelated;
using GoldenEggVine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated.LevelEntities
{
	public abstract class CLevelScreenExit : CLevelEntity
	{
		protected byte _positionx;
		protected byte _positiony;

		public CLevelScreenExit(ArraySegment<byte> screenexit) : base()
		{
			DeterminePosition(new SVector((byte)(screenexit.Array[screenexit.Offset] & 0x0F), (byte)((screenexit.Array[screenexit.Offset] & 0xF0) >> 4)));
			_positionx = screenexit.Array[screenexit.Offset + 2];
			_positiony = screenexit.Array[screenexit.Offset + 3];
		}

        public CLevelScreenExit(SVector position, byte positionx, byte positiony)
        {
            _position = position;
            _positionx = positionx;
            _positiony = positiony;
        }

		public virtual int GetDestinationLevel() { return -1; }

		public int GetDestinationX()
		{
			return _positionx;
		}

		public int GetDestinationY()
		{
			return _positiony;
		}

		public SVector GetDestinationVector()
		{
			return new SVector(_positionx, _positiony);
		}

		public override void DeterminePosition(SVector position)
		{
			if (position.InScreenExitBounds())
			{
				_position = position;
			}
			else
			{
				throw new Exception("Screen is supposed to be at page 0x" + String.Format("{0:X2}", position._x + (position._y << 4)) + ", which is not in the Level Area bounds!");
			}
		}

		public override int GetSize()
		{
			return 5;
		}

		public override void SetType()
		{
			_entitytype = ELevelEntityType.ScreenExit;
		}

        public bool IsAt(SVector pos)
        {
            return pos._x == this._position._x && pos._y == this._position._y;
        }

		public static void AddToListAndOverwrite(List<CLevelEntity> exitlist, CLevelScreenExit add)
		{
			foreach (var x in exitlist)
			{
				if (!(x is CLevelScreenExit))
				{
					throw new LevelEntityTypeMismatchException("In order to write ScreenExits to a List, every Object in the List must be of type CLevelScreenExit!");
				}
			}
			for (int i = 0; i < exitlist.Count; i++)
			{
				if (((CLevelScreenExit)exitlist[i])._position == add._position)
				{
					exitlist.Remove(exitlist[i]);
					--i;
				}
			}
			exitlist.Add(add);
		}

		public static CLevelScreenExit CreateScreenExitFromData(ArraySegment<byte> screenexit)
		{
			if(screenexit.Array[screenexit.Offset + 1] < CYIROM.NumberOfLevels())
			{
				return new CLevelLevelScreenExit(screenexit);
			}
			else if (screenexit.Array[screenexit.Offset + 1] < 0xE8)
			{
				return new CLevelMinibattleScreenExit(screenexit);
			}
			else
			{
				return null;
			}
		}

		public static CLevelScreenExit CreateScreenExitFromData(SVector position, byte destlevel, byte positionx, byte positiony, EEntranceType entrancetype)
		{
			return new CLevelLevelScreenExit(position, destlevel, positionx, positiony, entrancetype);
		}

		public static CLevelScreenExit CreateScreenExitFromData(SVector position, EMinibattle minibattle, byte returnlevel, byte positionx, byte positiony)
		{
			return new CLevelMinibattleScreenExit(position, minibattle, returnlevel, positionx, positiony);
		}

		/// <summary>
		/// Returns a Screen Exit at a given screen. If there is none, the returnvalue is null
		/// </summary>
		/// <param name="screenexits">List of all possible Screen Exits</param>
		/// <param name="screen">The screen to look for</param>
		/// <returns>The result</returns>
		public static CLevelScreenExit GetScreenExitAtScreen(List<CLevelEntity> screenexits, SVector screen)
		{
			foreach (CLevelScreenExit lse in screenexits)
			{
                if (lse.Exists())
                {
                    if (lse._position == screen)
                    {
                        return lse;
                    }
                }
			}
			return null;
		}
	}
}