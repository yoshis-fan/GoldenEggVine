using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public abstract class CLZCommand
	{
		protected byte[] _fulldata;
		protected int _startoffset;
		protected int _bytelength;
		protected int _length;

        public virtual void ExecuteCommand(List<byte> output) { }

		public int GetOffsetAfterCommand()
		{
			return _startoffset + _bytelength;
		}

		public static CLZCommand GenerateLZCommand(byte[] fulldata, int offset)
		{
			if (offset < 0 || offset > fulldata.Length)
			{
				throw new Exceptions.InvalidLZCommandExpression();
			}
			else
			{
				if (fulldata[offset] == 0xFF)
				{
					return new CStopCommand(fulldata, offset);
				}
				switch (fulldata[offset] >> 5)
				{
					case 0:
						return new CCopyCommand(fulldata, offset);
					case 1:
						return new CByteFillCommand(fulldata, offset);
					case 2:
						return new CWordFillCommand(fulldata, offset);
					case 3:
						return new CIncreasingFillCommand(fulldata, offset);
					case 4:
						return new CRepeatCommand(fulldata, offset);
					case 7:
						return new CLongLengthCommand(fulldata, offset);
					default:
						throw new Exceptions.InvalidLZCommandExpression();
				}
			}
		}
	}
}