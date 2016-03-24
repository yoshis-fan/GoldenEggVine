using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenEggVine.Exceptions
{
    public class RomSizeException : Exception
    {
        public RomSizeException() : base() { }

        public RomSizeException(string msg) : base(msg) { }
    }

    public class RomHeaderException : Exception
    {
        public RomHeaderException() : base() { }

        public RomHeaderException(string msg) : base(msg) { }
    }

    public class ObjectPointerException : Exception
    {
        public ObjectPointerException() : base() { }

        public ObjectPointerException(string msg) : base(msg) { }
    }

    public class SpritePointerException : Exception
    {
        public SpritePointerException() : base() { }

        public SpritePointerException(string msg) : base(msg) { }
    }

	public class EntranceDataAddressPointerException : Exception
	{
		public EntranceDataAddressPointerException() : base() { }

		public EntranceDataAddressPointerException(string msg) : base(msg) { }
	}

	public class MidwayDataAddressPointerException : Exception
	{
		public MidwayDataAddressPointerException() : base() { }

		public MidwayDataAddressPointerException(string msg) : base(msg) { }
	}

	public class InvalidTranslevelIndexException : Exception
	{
		public InvalidTranslevelIndexException() : base() { }

		public InvalidTranslevelIndexException(string msg) : base(msg) { }
	}

	public class InvalidItemMemoryIndexException : Exception
	{
		public InvalidItemMemoryIndexException() : base() { }

		public InvalidItemMemoryIndexException(string msg) : base(msg) { }
	}

	public class LevelEntityTypeMismatchException : Exception
	{
		public LevelEntityTypeMismatchException() : base() { }

		public LevelEntityTypeMismatchException(string msg) : base(msg) { }
	}

    public class InvalidPointerException : Exception
    {
        public InvalidPointerException() : base() { }

        public InvalidPointerException(string msg) : base(msg) { }
    }

	public class InvalidScreenExitTypeException : Exception
	{
		public InvalidScreenExitTypeException() : base() { }

		public InvalidScreenExitTypeException(string msg) : base(msg) { }
	}

	public class InvalidLZCommandExpression : Exception
	{
		public InvalidLZCommandExpression() : base() { }

		public InvalidLZCommandExpression(string msg) : base(msg) { }
	}
}