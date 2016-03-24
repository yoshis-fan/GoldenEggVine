using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public class CStopCommand : CLZCommand
	{
		private CStopCommand()
		{
			_bytelength = 0;
		}

		public CStopCommand(byte[] fulldata, int offset) : this()
		{
			_fulldata = fulldata;
			_startoffset = offset + 1;
			_length = fulldata[_startoffset] & 0x1F;
		}

		public CStopCommand(byte[] fulldata, int startoffset, int length) : this()
		{
			_fulldata = fulldata;
			_startoffset = startoffset;
			_length = length;
		}

		public override void ExecuteCommand(List<byte> output)
		{
			base.ExecuteCommand(output);
		}
	}
}