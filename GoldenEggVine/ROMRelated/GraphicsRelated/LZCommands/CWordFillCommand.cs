using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public class CWordFillCommand : CLZCommand
	{
		private CWordFillCommand()
		{
			_bytelength = 2;
		}

		public CWordFillCommand(byte[] fulldata, int offset) : this()
		{
			_fulldata = fulldata;
			_startoffset = offset + 1;
			_length = fulldata[_startoffset] & 0x1F;
		}

		public CWordFillCommand(byte[] fulldata, int startoffset, int length) : this()
		{
			_fulldata = fulldata;
			_startoffset = startoffset;
			_length = length;
		}

		public override void ExecuteCommand(List<byte> output)
		{
			for (int i = 0; i < _length; i++)
			{
				output.Add(((i & 1) == 0) ? _fulldata[_startoffset] : _fulldata[_startoffset + 1]);
			}
		}
	}
}