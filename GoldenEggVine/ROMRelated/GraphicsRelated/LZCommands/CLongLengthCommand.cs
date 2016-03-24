using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.ROMRelated.GraphicsRelated.LZCommands
{
	public class CLongLengthCommand : CLZCommand
	{
		private CLZCommand _realcommand;

		public CLongLengthCommand(byte[] fulldata, int offset)
		{
			_fulldata = fulldata;
			_startoffset = offset + 2;
			_length = (fulldata[offset] << 8) + fulldata[offset + 1];

			switch ((fulldata[offset] >> 2) & 7)
			{
				case 0:
					_realcommand = new CCopyCommand(_fulldata, _startoffset, _length);
					break;
				case 1:
					_realcommand = new CByteFillCommand(_fulldata, _startoffset, _length);
					break;
				case 2:
					_realcommand = new CWordFillCommand(_fulldata, _startoffset, _length);
					break;
				case 3:
					_realcommand = new CIncreasingFillCommand(_fulldata, _startoffset, _length);
					break;
				case 4:
					_realcommand = new CRepeatCommand(_fulldata, _startoffset, _length);
					break;
				default:
					throw new Exceptions.InvalidLZCommandExpression();
			}
		}

		public override void ExecuteCommand(List<byte> output)
		{
			_realcommand.ExecuteCommand(output);
		}
	}
}