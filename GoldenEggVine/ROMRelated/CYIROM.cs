using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using GoldenEggVine.ROMRelated.LevelRelated;
using GoldenEggVine.ROMRelated.TranslevelRelated;
using GoldenEggVine.Exceptions;
using GoldenEggVine.Helpers;

namespace GoldenEggVine.ROMRelated
{
	public enum EFileError { NONE, FILESIZE, ROMHEADER };
	public enum ELevelPointerError { NONE, OBJECT, SPRITE };
	public enum EEntrancePointerError { NONE, DATAADDRESS };
	public enum EMidwayPointerError { NONE, DATAADDRESS };
	public enum ELevelData { OBJECTS, SPRITES };

	/// <summary>
	/// Completelevel: Binary Data, Levelspecs
	/// </summary>
	public struct SCompleteLevel
	{
		public byte[] levelbytes;
		public SLevelspecs levelinfo;

		public SCompleteLevel(byte[] levelbytes, SLevelspecs levelinfo)
		{
			this.levelbytes = levelbytes;
			this.levelinfo = levelinfo;
		}
	}

	/// <summary>
	/// Levelspecs: Index, Objects/Sprites, Address, Length
	/// </summary>
	public struct SLevelspecs
	{
		public int levelindex;
		public ELevelData leveldata;
		public int startaddress;
		public int length;

		public SLevelspecs(int levelindex, ELevelData leveldata, int startaddress, int length)
		{
			this.levelindex = levelindex;
			this.leveldata = leveldata;
			this.startaddress = startaddress;
			this.length = length;
		}
		public override string ToString()
		{
			return "Levelindex: " + String.Format("{0:X2}", levelindex) + "\nDataType: " + (leveldata == ELevelData.OBJECTS ? "Objects" : "Sprites") + "\nStartaddress: 0x" + String.Format("{0:X2}", startaddress) + "\nLength: " + length + " bytes";
		}
	}

	/// <summary>
	/// Freespace: Address, Length
	/// </summary>
	public struct SFreeSpace
	{
		public int startaddress;
		public int length;

		public SFreeSpace(int startaddress, int length)
		{
			this.startaddress = startaddress;
			this.length = length;
		}
	}

	/// <summary>
	/// Allrombytes, Objectpointers, Spritepointers, Renderlengthes
	/// </summary>
	public class CYIROM
	{
		/// <summary>
		/// The number of available translevels (meaning translevels 0 up to this number minus 1 are valid).
		/// </summary>
		public const int NumTransLevels = 0x48;

		/// <summary>
		/// The number of valid level indices.
		/// </summary>
		public const int NumLevelIndices = 0xDE;

		public const int NumScreensPerLevel = 0x10 * 0x8;

		internal static byte[] headerromproperties = new byte[] { 0x15, 0x0B, 0x00, 0x01, 0x33, 0x00 };
		internal static byte[] headerinterrupt = new byte[] { 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x08, 0x01, 0x4F, 0x81, 0x0C, 0x01, 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x4F, 0x81, 0x00, 0x80, 0x4F, 0x81 };
		public byte[] rombytes;

		private byte[] renderlength = new byte[0x100];
		private static uint objectrendertable = 0x906EC;
		private static byte[] renderfour = { 0x00, 0x01, 0x41, 0x80, 0xC0, 0xFF };
		private static byte[] renderfive = { 0x02, 0x42, 0x82, 0xC2 };



		private static int itemsallowedoffset = 0x71C;

		private static int levelmodeparametersoffset = 0x3E12;

		private static int bg2horizontalscrolloffset = 0x27D6E;
		private static int bg3horizontalscrolloffset = 0x27DEE;
		private static int bg4horizontalscrolloffset = 0x27E6E;
		private static int bg2verticalscrolloffset = 0x27DAE;
		private static int bg3verticalscrolloffset = 0x27E2E;
		private static int bg4verticalscrolloffset = 0x27EAE;

		private static int spritetilesets = 0x3239;

		//Are auto-filled when initialising the YIROM-Object
		private List<CLevelData> leveldata;
		private List<CEntranceData> entrancedata;
		private List<CMidwayData> midwaydata;
		//Must be updated frequently
		private List<CTranslevelLevelConnection> levelconnections;

		//Absolute Pointers
		private int objectpointers;
		private int spritepointers;

		//Relative Pointers
		private int entrancepointers;
		private int entrancepointerdata;

		private int midwaypointers;
		private int midwaypointerdata;



		/*public CYIROM(CYIROM tocopy)
        {
            this.rombytes = new byte[tocopy.rombytes.Length];
            Array.Copy(tocopy.rombytes, this.rombytes, tocopy.rombytes.Length);
            ValidateROM();
            SetAndValidateLevelPointers();
        }*/

		public CYIROM(string filepath)
		{
			try
			{
				this.rombytes = File.ReadAllBytes(filepath);
			}
			catch (Exception)
			{
				throw new FileLoadException("Error reading the file at path\n"
								+ filepath
								+ "\nMake sure you have reading and writing permissions to the file!");
			}
			switch (ValidateROM())
			{
				case EFileError.FILESIZE:
					throw new RomSizeException("The size of the ROM is incorrect. Maybe the header is missing?\n"
											   + "In that case, the file size is 2,097,152 bytes.");
				case EFileError.ROMHEADER:
					throw new RomHeaderException("The header of the ROM is incorrect. Maybe it is not a 'Yoshi's Island'-ROM?");
			}
			switch (SetAndValidateLevelPointers())
			{
				case ELevelPointerError.OBJECT:
					throw new ObjectPointerException("There is something wrong with the Object Pointer Data in the ROM. Unable to load Object Data.");
				case ELevelPointerError.SPRITE:
					throw new SpritePointerException("There is something wrong with the Sprite Pointer Data in the ROM. Unable to load Sprite Data.");
			}
			switch (SetAndValidateEntranceDataAndPointers())
			{
				case EEntrancePointerError.DATAADDRESS:
					throw new EntranceDataAddressPointerException("There is something wrong with the Entrance Data Pointer Data in the ROM. Unable to load Entrance Data.");
			}
			switch (SetAndValidateMidwayDataAndPointers())
			{
				case EMidwayPointerError.DATAADDRESS:
					throw new MidwayDataAddressPointerException("There is something wrong with the Midway Data Pointer Data in the ROM. Unable to load Midway Data.");
			}

			CreateRenderLengthTable();

			this.leveldata = new List<CLevelData>();
			for (int i = 0; i < NumLevelIndices; i++)
			{
				try
				{
					this.leveldata.Add(this.GenerateLevelData(i));
				}
				catch (InvalidPointerException)
				{
					throw new InvalidPointerException("The Pointer for Leveldata of Level 0x" + String.Format("{0:X2}", i) + " is invalid! PC Pointer Address: 0x" + String.Format("{0:X6}", objectpointers + 6 * i));
				}
			}

			this.levelconnections = new List<CTranslevelLevelConnection>();
			/*
			for (int i = 0; i < numberoftranslevels; i++)
			{
				this.levelconnections.Add(new CTranslevelLevelConnection(this, i));
			}*/
		}

		private void CreateRenderLengthTable()
		{
			for (uint i = 0; i < 0x100; i++)
			{
				renderlength[i] = GetRenderBytes((byte)i);
			}
		}

		private EFileError ValidateROM()
		{
			//Check filesize
			if (rombytes.Length != 0x200200)
			{
				return EFileError.FILESIZE;
			}
			//Check ROM-Header
			for (int i = 0; i < 6; i++)
			{
				if (headerromproperties[i] != rombytes[0x81D6 + i])
				{
					return EFileError.ROMHEADER;
				}
			}
			for (int i = 0; i < 32; i++)
			{
				if (headerinterrupt[i] != rombytes[0x81E0 + i])
				{
					return EFileError.ROMHEADER;
				}
			}
			return EFileError.NONE;
		}

		private ELevelPointerError SetAndValidateLevelPointers()
		{
			int objbyte1 = rombytes[0xB28F] + rombytes[0xB290] * 0x100 + rombytes[0xB291] * 0x10000;
			int objbyte2 = rombytes[0xB295] + rombytes[0xB296] * 0x100 + rombytes[0xB297] * 0x10000;

			int sprbyte1 = rombytes[0xB29B] + rombytes[0xB29C] * 0x100 + rombytes[0xB29D] * 0x10000;
			int sprbyte3 = rombytes[0xB2A3] + rombytes[0xB2A4] * 0x100 + rombytes[0xB2A5] * 0x10000;

			if (objbyte1 + 1 != objbyte2)
			{
				return ELevelPointerError.OBJECT;
			}
			else
			{
				this.objectpointers = LoROMPointerToPCAddress(objbyte1);
			}
			if (sprbyte1 + 2 != sprbyte3)
			{
				return ELevelPointerError.SPRITE;
			}
			else
			{
				this.spritepointers = LoROMPointerToPCAddress(sprbyte1);
			}
			return ELevelPointerError.NONE;
		}

		private EEntrancePointerError SetAndValidateEntranceDataAndPointers()
		{
			this.entrancepointers = LoROMPointerAddressToPCAddress(0xB1F4);

			int byte2 = rombytes[0xB1F9] + rombytes[0xB1FA] * 0x100 + rombytes[0xB1FB] * 0x10000;
			int byte3 = rombytes[0xB207] + rombytes[0xB208] * 0x100 + rombytes[0xB209] * 0x10000;
			int byte1 = rombytes[0xB215] + rombytes[0xB216] * 0x100 + rombytes[0xB217] * 0x10000;

			if (byte1 + 1 != byte2 || byte1 + 2 != byte3 || byte2 + 1 != byte3)
			{
				return EEntrancePointerError.DATAADDRESS;
			}
			else
			{
				this.entrancepointerdata = LoROMPointerToPCAddress(byte1);
			}

			this.entrancedata = new List<CEntranceData>();
			for (int i = 0; i < CYIROM.NumTransLevels; i++)
			{
				this.entrancedata.Add(new CEntranceData(this.rombytes, i, this.entrancepointers, this.entrancepointerdata));
			}

			return EEntrancePointerError.NONE;
		}

		private EMidwayPointerError SetAndValidateMidwayDataAndPointers()
		{
			this.midwaypointers = LoROMPointerToPCAddress(0x170000 + rombytes[0xE86A] + rombytes[0xE86B] * 0x100);

			int byte2 = 0x170000 + rombytes[0xE86E] + rombytes[0xE86F] * 0x100;
			int byte1 = 0x170000 + rombytes[0xE877] + rombytes[0xE878] * 0x100;
			int byte4 = 0x170000 + rombytes[0xE87E] + rombytes[0xE87F] * 0x100;

			if (byte1 + 1 != byte2 || byte1 + 3 != byte4 || byte2 + 2 != byte4)
			{
				return EMidwayPointerError.DATAADDRESS;
			}
			else
			{
				this.midwaypointerdata = LoROMPointerToPCAddress(byte1);
			}

			this.midwaydata = new List<CMidwayData>();
			for (int i = 0; i < CYIROM.NumTransLevels; i++)
			{
				this.midwaydata.Add(new CMidwayData(this.rombytes, i, this.midwaypointers, this.midwaypointerdata));
			}

			return EMidwayPointerError.NONE;
		}

		private byte GetRenderBytes(byte objectindex)
		{
			if (renderfour.Contains(this.rombytes[objectrendertable + objectindex]))
			{
				return (byte)4;
			}
			else if (renderfive.Contains(this.rombytes[objectrendertable + objectindex]))
			{
				return (byte)5;
			}
			return byte.MaxValue;
		}

		/// <summary>
		/// Gets the Leveldata from a specified Level
		/// </summary>
		/// <param name="levelindex">Levelindex to get Leveldata from</param>
		/// <returns>The Leveldata from that Levelindex</returns>
		private CLevelData GenerateLevelData(int levelindex)
		{
			if (levelindex > -1 && levelindex < NumLevelIndices)
			{
				return new CLevelData(new ArraySegment<byte>(rombytes, (int)LoROMPointerAddressToPCAddress(this.objectpointers + 6 * levelindex), 0), new ArraySegment<byte>(rombytes, (int)LoROMPointerAddressToPCAddress(this.spritepointers + 6 * levelindex), 0), renderlength, levelindex);
			}
			else
			{
				throw new InvalidPointerException("Pointers to Levels smaller than 0x00 or greater than 0xDD cannot be resolved! Level Data of Level 0x" + String.Format("{0:X2}", levelindex) + " was requested.");
			}
		}

		/// <summary>
		/// Gets Address and Length of a Level from Levelindex and Type of Leveldata
		/// </summary>
		/// <param name="levelindex">Levelindex of the Level</param>
		/// <param name="leveldata">Type of Leveldata (Objects/Sprites)</param>
		/// <returns>Struct with Levelindex, Type of Leveldata, Address, Length</returns>
		public SLevelspecs GetLevelSpecs(int levelindex, ELevelData leveldata)
		{
			SLevelspecs level;
			level.levelindex = levelindex;
			level.leveldata = leveldata;
			if (levelindex > 0xDD)
			{
				level.startaddress = 0;
				level.length = 0;
				return level;
			}
			switch (leveldata)
			{
				case ELevelData.OBJECTS:
					level.startaddress = LoROMPointerAddressToPCAddress(this.objectpointers + 6 * levelindex);
					if (level.startaddress == 0x200200)
					{
						throw new InvalidPointerException("The Pointer for the Object-Data of Level 0x"
															+ String.Format("{0:X2}", levelindex)
															+ " is invalid!\n"
															+ "You will receive a fake-level with no data in it!");
					}
					int currentaddress1 = level.startaddress + 10;
					while (this.rombytes[currentaddress1] != 0xFF)
					{
						currentaddress1 += GetRenderBytes(this.rombytes[currentaddress1]);
					}
					currentaddress1++;
					while (this.rombytes[currentaddress1] != 0xFF)
					{
						currentaddress1 += 5;
					}
					level.length = currentaddress1 - level.startaddress + 1;
					return level;
				case ELevelData.SPRITES:
					level.startaddress = LoROMPointerAddressToPCAddress(this.spritepointers + 6 * levelindex);
					if (level.startaddress == 0x200200)
					{
						throw new InvalidPointerException("The Pointer for the Sprite-Data of Level 0x"
															+ String.Format("{0:X2}", levelindex)
															+ " is invalid!\n"
															+ "You will receive a fake-level with no data in it!");
					}
					int currentaddress2 = level.startaddress;
					while (this.rombytes[currentaddress2] != 0xFF || this.rombytes[currentaddress2 + 1] != 0xFF)
					{
						currentaddress2 += 3;
					}
					currentaddress2++;
					level.length = currentaddress2 - level.startaddress + 1;
					return level;
				default:
					level.startaddress = 0;
					level.length = 0;
					return level;
			}
		}

		/// <summary>
		/// Gets Address a 3-byte Pointer at an Address points to
		/// 
		/// throws: InvalidPointerException
		/// </summary>
		/// <param name="address">Address of the Pointer</param>
		/// <returns>Address the Pointer at that Address points to</returns>
		public int LoROMPointerAddressToPCAddress(int address)
		{
			if (address + 2 < this.rombytes.Length)
			{
				return LoROMPointerToPCAddress(this.rombytes[address] + this.rombytes[address + 1] * 0x100 + this.rombytes[address + 2] * 0x10000);
			}
			return 0;
		}

		/// <summary>
		/// Gets PC Address from 3-byte Pointer
		/// 
		/// throws: InvalidPointerException
		/// </summary>
		/// <param name="orderedpointer">Pointer already sorted</param>
		/// <returns>Address the Pointer points to</returns>
		public static int LoROMPointerToPCAddress(int orderedpointer)
		{
			if ((orderedpointer >> 16) > 0x3F)
			{
				while ((orderedpointer >> 16) > 0x3F)
				{
					orderedpointer -= 0x400000;
				}
				return orderedpointer + 0x200;
			}
			if ((orderedpointer & 0xFF00) < 0x8000)
			{
				throw new InvalidPointerException("The specified Pointer is in SNES Format, however, the Address is below 0x8000");
			}
			if (((orderedpointer >> 16) & 0x1) == 0)
			{
				return orderedpointer - ((orderedpointer & 0xFF0000) >> 1) - 0x7E00;
			}
			else
			{
				return orderedpointer - (((orderedpointer + 0x10000) & 0xFF0000) >> 1) + 0x200;
			}
		}

		/// <summary>
		/// Gets the Levelbytes from a Level using the Levelspecs (Levelindex, Type of Leveldata, Address, Length)
		/// </summary>
		/// <param name="level">Levelspecs</param>
		/// <returns>Byte Array containing the binary data of the Level</returns>
		public byte[] GetLevelBytes(SLevelspecs level)
		{
			return CSubArray.StartLength(this.rombytes, (int)level.startaddress, (int)level.length);
		}

		/// <summary>
		/// Erases a Level in the ROM
		/// </summary>
		/// <param name="level">Levelspecs</param>
		public void EraseLevelBytes(SLevelspecs level)
		{
			int currentaddress = level.startaddress;
			for (int i = 0; i < level.length; i++, currentaddress++)
			{
				this.rombytes[currentaddress] = 0xFF;
			}
			currentaddress++;
			while (this.rombytes[currentaddress] == 0x00)
			{
				this.rombytes[currentaddress] = 0xFF;
				currentaddress++;
			}
		}

		/// <summary>
		/// Gets all chunks of Freespace in the ROM larger than a specified Threshold
		/// </summary>
		/// <param name="threshold">The Threshold</param>
		/// <returns>List of Freespace</returns>
		public List<SFreeSpace> GetFreeSpace(uint threshold)
		{
			List<SFreeSpace> freespacelist = new List<SFreeSpace>();
			for (int i = 200; i < this.rombytes.Length - 1; i++)
			{
				if (this.rombytes[i] == 0xFF && this.rombytes[i + 1] == 0xFF)
				{
					i += 2;
					int length = 0;
					SFreeSpace fs;
					fs.startaddress = i;
					while (this.rombytes[i] == 0xFF)
					{
						length++;
						i++;
						if (i == this.rombytes.Length)
						{
							break;
						}
					}
					fs.length = length;
					if (fs.length + 1 > threshold)
					{
						freespacelist.Add(fs);
					}
				}
			}
			return freespacelist;
		}

		/// <summary>
		/// Returns, whether a Level is valid or not
		/// </summary>
		/// <param name="level">Binary Leveldata</param>
		/// <param name="leveldata">Type of Leveldata (Objects/Sprites)</param>
		/// <returns>True, if Data is valid</returns>
		public bool ValidateLevel(byte[] level, ELevelData leveldata)
		{
			switch (leveldata)
			{
				case ELevelData.OBJECTS:
					if (level.Length < 12)
					{
						return false;
					}
					uint i = 10;
					while (i < level.Length - 2)
					{
						if (level[i] == 0xFF)
						{
							break;
						}
						else
						{
							i += GetRenderBytes(level[i]);
						}

					}
					i++;
					if (level[i] != 0xFF)
					{
						while (i < level.Length - 1)
						{
							i += 5;
						}
					}
					if (i == level.Length - 1 && level[i] == 0xFF)
					{
						return true;
					}
					return false;
				case ELevelData.SPRITES:
					if (level.Length < 2)
					{
						return false;
					}
					for (int j = 0; j < level.Length - 1; j += 3)
					{
						if (level[j] == 0xFF && level[j + 1] == 0xFF && j != level.Length - 2)
						{
							return false;
						}
					}
					if (level[level.Length - 2] != 0xFF || level[level.Length - 1] != 0xFF)
					{
						return false;
					}
					return true;
				default:
					return false;
			}
		}

		public byte[] GetSpriteGFXFiles(int index)
		{
			if (index < 0 || index > 0x7F)
			{
				throw new Exception("");
			}
			return new byte[] { rombytes[spritetilesets + index * 6 + 0], rombytes[spritetilesets + index * 6 + 1], rombytes[spritetilesets + index * 6 + 2], rombytes[spritetilesets + index * 6 + 3], rombytes[spritetilesets + index * 6 + 4], rombytes[spritetilesets + index * 6 + 5] };
		}

		public void SetSpriteGFXFiles(int index, byte[] values)
		{
			if (values.Length != 6)
			{
				throw new Exception("");
			}
			if (index < 0 || index > 0x7F)
			{
				throw new Exception("");
			}
			for (int i = 0; i < values.Length; i++)
			{
				rombytes[spritetilesets + i] = values[i];
			}
		}

		public bool GetItemsAllowed(int musicindex)
		{
			if (musicindex > -1 && musicindex < 0x10)
			{
				return rombytes[itemsallowedoffset + musicindex] == 0x00;
			}
			else
			{
				throw new Exception("Invalid Track-Index");
			}
		}

		public byte[] GetLevelModeParams(int levelmodeindex)
		{
			if (levelmodeindex > -1 && levelmodeindex < LevelRelated.CLevelHeader.GetHeaderValueRange(EHeaderValue.LevelMode))
			{
				byte[] levelmodeparams = new byte[0x14];
				for (int i = 0; i < levelmodeparams.Length; i++)
				{
					levelmodeparams[i] = rombytes[levelmodeparametersoffset + levelmodeindex * 0x14 + i];
				}
				return levelmodeparams;
			}
			else
			{
				throw new Exception("Invalid Level-Mode-Index");
			}
		}

		public short[] GetBGScrollingParams(int bgscrollingindex)
		{
			if (bgscrollingindex > -1 && bgscrollingindex < LevelRelated.CLevelHeader.GetHeaderValueRange(EHeaderValue.BGScrolling))
			{
				//Each value is a Signed Short

				short[] bgscrollingparams = new short[0x6];

				unchecked
				{
					//BG2 Horizontal Scrolling
					bgscrollingparams[0] = (short)(rombytes[bg2horizontalscrolloffset + 2 * bgscrollingindex] + (rombytes[bg2horizontalscrolloffset + 2 * bgscrollingindex + 1] << 8));
					//BG2 Vertical Scrolling
					bgscrollingparams[1] = (short)(rombytes[bg2verticalscrolloffset + 2 * bgscrollingindex] + (rombytes[bg2verticalscrolloffset + 2 * bgscrollingindex + 1] << 8));
					//BG3 Horizontal Scrolling
					bgscrollingparams[2] = (short)(rombytes[bg3horizontalscrolloffset + 2 * bgscrollingindex] + (rombytes[bg3horizontalscrolloffset + 2 * bgscrollingindex + 1] << 8));
					//BG3 Vertical Scrolling
					bgscrollingparams[3] = (short)(rombytes[bg3verticalscrolloffset + 2 * bgscrollingindex] + (rombytes[bg3verticalscrolloffset + 2 * bgscrollingindex + 1] << 8));
					//BG4 Horizontal Scrolling
					bgscrollingparams[4] = (short)(rombytes[bg4horizontalscrolloffset + 2 * bgscrollingindex] + (rombytes[bg4horizontalscrolloffset + 2 * bgscrollingindex + 1] << 8));
					//BG4 Vertical Scrolling
					bgscrollingparams[5] = (short)(rombytes[bg4verticalscrolloffset + 2 * bgscrollingindex] + (rombytes[bg4verticalscrolloffset + 2 * bgscrollingindex + 1] << 8));
				}

				return bgscrollingparams;
			}
			else
			{
				throw new Exception("Invalid BG-Scrolling-Index");
			}
		}

		public void SetItemsAllowed(int musicindex, bool value)
		{
			rombytes[itemsallowedoffset + musicindex] = value ? (byte)0x00 : (byte)0x01;
		}

		public List<CEntranceData> GetEntranceData()
		{
			return this.entrancedata;
		}

		public List<CMidwayData> GetMidwayData()
		{
			return this.midwaydata;
		}

		public List<CLevelData> GetAllLevelData()
		{
			return this.leveldata;
		}

		public CLevelData GetLevelData(int levelindex)
		{
			if (levelindex > -1 && levelindex < leveldata.Count)
			{
				return leveldata[levelindex];
			}
			else
			{
				throw new Exception("Data of the Level " + String.Format("{0:X2}", levelindex) + " was requested, but the List only contains " + String.Format("{0:X2}", leveldata.Count) + " entries.");
			}
		}

		public List<CLevelData> AffectedLevels(EHeaderValue headervalue, int value)
		{
			List<CLevelData> affected = new List<CLevelData>();
			foreach (CLevelData ld in leveldata)
			{
				if (ld.LevelHeader().GetHeaderValue(headervalue) == value)
				{
					affected.Add(ld);
				}
			}
			return affected;
		}
	}
}