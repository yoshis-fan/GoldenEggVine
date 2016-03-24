using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public class CCopyCommand : CLZCommand
	{
		private CCopyCommand() { }

		public CCopyCommand(byte[] fulldata, int offset) : this()
		{
			_fulldata = fulldata;
			_startoffset = offset + 1;
			_length = (fulldata[offset] & 0x1F) + 1;
			_bytelength = _length;
		}

		public CCopyCommand(byte[] fulldata, int startoffset, int length) : this()
		{
			_fulldata = fulldata;
			_startoffset = startoffset;
			_length = length;
			_bytelength = _length;
		}

		public override void ExecuteCommand(List<byte> output)
		{
			for (int i = 0; i < _length; i++)
			{
				output.Add(_fulldata[_startoffset + i]);
			}
		}
	}
}