using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public class CRepeatCommand : CLZCommand
	{
		private CRepeatCommand()
		{
			_bytelength = 2;
		}

		public CRepeatCommand(byte[] fulldata, int offset) : this()
		{
			_fulldata = fulldata;
			_startoffset = offset + 1;
			_length = fulldata[_startoffset] & 0x1F;
		}

		public CRepeatCommand(byte[] fulldata, int startoffset, int length) : this()
		{
			_fulldata = fulldata;
			_startoffset = startoffset;
			_length = length;
		}

		public override void ExecuteCommand(List<byte> output)
		{
			for (int i = 0; i < _length; i++)
			{
				output.Add(output[_fulldata[_startoffset] + (_fulldata[_startoffset + 1] << 8) + i]);
			}
		}
	}
}
