using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public class CByteFillCommand : CLZCommand
	{
		private CByteFillCommand()
		{
			_bytelength = 1;
		}

		public CByteFillCommand(byte[] fulldata, int offset) : this()
		{
			_fulldata = fulldata;
			_startoffset = offset + 1;
			_length = fulldata[offset] & 0x1F;
		}

		public CByteFillCommand(byte[] fulldata, int startoffset, int length) : this()
		{
			_fulldata = fulldata;
			_startoffset = startoffset;
			_length = length;
		}

		public override void ExecuteCommand(List<byte> output)
		{
			for (int i = 0; i < _length; i++)
			{
				output.Add(_fulldata[_startoffset]);
			}
		}
	}
}