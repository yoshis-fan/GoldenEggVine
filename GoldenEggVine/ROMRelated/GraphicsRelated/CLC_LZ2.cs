using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated
{
	public class CLC_LZ2
	{
		public static byte[] ProcessRawData(byte[] fulldata, int offset)
		{
			List<byte> output = new List<byte>();
			LZCommands.CLZCommand lzcommand = LZCommands.CLZCommand.GenerateLZCommand(fulldata, offset);
			while(!(lzcommand is LZCommands.CStopCommand))
			{
				lzcommand.ExecuteCommand(output);
				lzcommand = LZCommands.CLZCommand.GenerateLZCommand(fulldata, lzcommand.GetOffsetAfterCommand());
			}
			return output.ToArray();
		}
	}
}