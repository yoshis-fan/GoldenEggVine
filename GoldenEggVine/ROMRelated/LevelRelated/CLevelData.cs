using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GoldenEggVine.ROMRelated.LevelRelated
{
	public class CLevelData
	{
		private int _levelindex;
		private int _objectspace;
		private int _spritespace;
		private CLevelHeader _header;
		private List<CLevelEntity> _objects = new List<CLevelEntity>();
		private List<CLevelEntity> _screenexits = new List<CLevelEntity>();
		private List<CLevelEntity> _sprites = new List<CLevelEntity>();

		public CLevelData(ArraySegment<byte> rawobjects, ArraySegment<byte> rawsprites, byte[] rendertable, int levelindex)
		{
			_levelindex = levelindex;
			//Get first 10 bytes of objects (header)
			_header = new CLevelHeader(new ArraySegment<byte>(rawobjects.Array, rawobjects.Offset, 10));
			int objectpos = 10;
			_objectspace = 10;
			//Get Objects and XObjects
			while (rawobjects.Array[rawobjects.Offset + objectpos] != 0xFF)
			{
				_objects.Add(CLevelObject.CreateObject(GetObjectType(rawobjects.Array[rawobjects.Offset + objectpos], rendertable), new ArraySegment<byte>(rawobjects.Array, rawobjects.Offset + objectpos, 5)));
				objectpos += _objects.Last().GetSize();
				_objectspace += _objects.Last().GetSize();
			}
			objectpos++;
			//Get Screen Exits
			while (rawobjects.Array[rawobjects.Offset + objectpos] != 0xFF)
			{
				try
				{
					CLevelScreenExit.AddToListAndOverwrite(_screenexits, CLevelScreenExit.CreateScreenExitFromData(new ArraySegment<byte>(rawobjects.Array, rawobjects.Offset + objectpos, 5)));
				}
				catch (Exception)
				{

				}
				objectpos += 5;
				_objectspace += 5;
			}
			objectpos++;
			//Get Freespace afterwards
			while (rawobjects.Array[rawobjects.Offset + objectpos] == 0xFF)
			{
				objectpos++;
				_objectspace++;
			}
			int spritepos = 0;
			_spritespace = 0;
			//Get Sprites
			while (rawsprites.Array[rawsprites.Offset + spritepos] != 0xFF || rawsprites.Array[rawsprites.Offset + spritepos + 1] != 0xFF)
			{
				_sprites.Add(new CLevelSprite(new ArraySegment<byte>(rawsprites.Array, rawsprites.Offset + spritepos, 3)));
				spritepos += 3;
				_spritespace += 3;
			}
			spritepos += 2;
			while (rawsprites.Array[rawsprites.Offset + spritepos] == 0xFF)
			{
				spritepos++;
				_spritespace++;
			}
		}

		public int GetLevelIndex()
		{
			return _levelindex;
		}

		public int GetObjectSpace()
		{
			return _objectspace;
		}

		public int GetSpriteSpace()
		{
			return _spritespace;
		}

		public List<CLevelEntity> Objects()
		{
			return _objects;
		}

		public List<CLevelEntity> Sprites()
		{
			return _sprites;
		}

		public List<CLevelEntity> ScreenExits()
		{
			return _screenexits;
		}

		public CLevelHeader LevelHeader()
		{
			return _header;
		}

		public void AddNewEntity(CLevelEntity entity)
		{
			if (entity is CLevelObject)
			{
				_objects.Add(entity);
			}
			else if (entity is CLevelSprite)
			{
				_sprites.Add(entity);
			}
			else if (entity is CLevelScreenExit)
			{
				_screenexits.Add(entity);
			}
			else
			{
				throw new GoldenEggVine.Exceptions.LevelEntityTypeMismatchException();
			}
		}

		private ELevelObjectType GetObjectType(byte index, byte[] rendertable)
		{
			if (index == 0)
			{
				return ELevelObjectType.Extended;
			}
			else
			{
				return rendertable[index] == 4 ? ELevelObjectType.OneDimensional : ELevelObjectType.TwoDimensional;
			}
		}
	}
}