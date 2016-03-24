using GoldenEggVine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.LevelRelated
{
	public enum EHeaderValue { BGColor=0, BG1TS=1, BG1Pal=2, BG2TS=3, BG2Pal=4, BG3TS=5, BG3Pal=6, SprTS=7, SprPal=8, LevelMode=9, AnimationTS=10, AnimationPal=11, BGScrolling=12, Music=13, ItemMemory=14, Unused=15 };

	public class CLevelHeader
	{
		private static int[] _valuestarts = new int[] { 0, 5, 9, 14, 19, 25, 31, 37, 44, 48, 53, 59, 64, 69, 73, 75 };
		private static int[] _valuelengths = new int[] { 5, 4, 5, 5, 6, 6, 6, 7, 4, 5, 6, 5, 5, 4, 2, 5 };

		private byte[] _rawheaderdata;
        //    TYPE              LEN      BYTE:BIT
		// BG Color              5       0:0 - 0:4
		// BG 1 Tileset          4       0:5 - 1:0
		// BG 1 Palette          5       1:1 - 1:5
		// BG 2 Tileset          5       1:6 - 2:2
		// BG 2 Palette          6       2:3 - 3:0
		// BG 3 Tileset          6       3:1 - 3:6
		// BG 3 Palette          6       3:7 - 4:4
		// Sprite Tileset        7       4:5 - 5:3
		// Sprite Palette        4       5:4 - 5:7
		// Level Mode            5       6:0 - 6:4
		// Animation Tileset     6       6:5 - 7:2
		// Animation Palette     5       7:3 - 7:7
		// BG Scrolling          5       8:0 - 8:4
		// Music                 4       8:5 - 9:0
		// Item Memory           2       9:1 - 9:2
		// Unused                5       9:3 - 9:7

		public CLevelHeader(ArraySegment<byte> headerbytes)
		{
			_rawheaderdata = new byte[10];
			for (int i = 0; i < _rawheaderdata.Length; i++)
			{
				_rawheaderdata[i] = headerbytes.Array[headerbytes.Offset + i];
			}
			/*
			bool[] headerbits = BytesToBoolArray(headerbytes);
			_backgroundcolor = new CBits(ValueFromBits(headerbits, 0, 5), 5);
			_bg1tileset = new CBits(ValueFromBits(headerbits, 5, 4), 4);
			_bg1palette = new CBits(ValueFromBits(headerbits, 9, 5), 5);
			_bg2tileset = new CBits(ValueFromBits(headerbits, 14, 5), 5);
			_bg2palette = new CBits(ValueFromBits(headerbits, 19, 6), 6);
			_bg3tileset = new CBits(ValueFromBits(headerbits, 25, 6), 6);
			_bg3palette = new CBits(ValueFromBits(headerbits, 31, 6), 6);
			_spritetileset = new CBits(ValueFromBits(headerbits, 37, 7), 7);
			_spritepalette = new CBits(ValueFromBits(headerbits, 44, 4), 4);
			_levelmode = new CBits(ValueFromBits(headerbits, 48, 5), 5);
			_animationtileset = new CBits(ValueFromBits(headerbits, 53, 6), 6);
			_animationpalette = new CBits(ValueFromBits(headerbits, 59, 5), 5);
			_bgscrolling = new CBits(ValueFromBits(headerbits, 64, 5), 5);
			_music = new CBits(ValueFromBits(headerbits, 69, 4), 4);
			_itemmemory = new CBits(ValueFromBits(headerbits, 73, 2), 2);

            _unused = new CBits(ValueFromBits(headerbits, 75, 5), 5);
			 * */
		}

		public int GetHeaderValue(EHeaderValue headervalue)
		{
			return CBitManipulator.GetValue(_rawheaderdata, _valuestarts[(int)headervalue], _valuelengths[(int)headervalue]);
		}

		public void SetHeaderValue(EHeaderValue headervalue, int value)
		{
			CBitManipulator.SetValue(_rawheaderdata, value, _valuestarts[(int)headervalue], _valuelengths[(int)headervalue]);
		}

		public static int GetHeaderValueLength(EHeaderValue headervalue)
		{
			return CLevelHeader._valuelengths[(int)headervalue];
		}

		public static int GetHeaderValueRange(EHeaderValue headervalue)
		{
			return 1 << CLevelHeader._valuelengths[(int)headervalue];
		}

		public static int GetHeaderValueStartBit(EHeaderValue headervalue)
		{
			return CLevelHeader._valuestarts[(int)headervalue];
		}

		private static bool IsInRange(EHeaderValue headervalue, int n)
		{
			//If the value is between 0 and 2^(length_of_value) - 1, the value is in range (true)
			return (n > -1 && n < (1 << _valuelengths[(int)headervalue]));
		}





		public bool[] BytesToBoolArray(ArraySegment<byte> bytes)
		{
			bool[] bits = new bool[8 * bytes.Count];
			for (int i = 0; i < bytes.Count; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					bits[i*8 + j] = ((((bytes.Array[bytes.Offset + i]) << j) & 0x80) >> 7) == 1;
				}
			}
			return bits;
		}

        private byte[] HeaderBitsToByteArray(bool[] bits)
        {
            if(bits.Length != 0x50)
            {
                throw new Exception("That's not a valid header. Valid header consist of 80 Bits!");
            }
            byte[] bytes = new byte[bits.Length >> 3];
            int processbool = 0;
            for (int i = 0; i < 10; i++, processbool += 8)
            {
                bytes[i] = 0x00;
                for(int j = 0; j < 8; j++)
                {
                    bytes[i] += (byte)(bits[processbool + j] ? (1 << (7 - j)) : 0);
                }
            }
            return bytes;
        }

		public byte[] GetByteArray()
		{
			return _rawheaderdata;
		}

		public byte ValueFromBits(bool[] b, int positionfromhighest, int length)
		{
			byte result = 0;
			for (int i = 0; i < length; i++)
			{
				result += (byte)((b[positionfromhighest + i] ? 1 : 0) << (length - 1 - i));
			}
			return result;
		}
	}
}