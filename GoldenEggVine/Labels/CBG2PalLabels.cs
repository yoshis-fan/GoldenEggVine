using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EBG2PalLabelsContent { INDEX = 0, HINT = 1, DESCRIPTION = 2 };

	public class CBG2PalLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EBG2PalLabelsContent)).Cast<EBG2PalLabelsContent>().Max() + 1;
		private static int _entries = 0x40;

		public CBG2PalLabels(string[] parsecontent)
			: base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EBG2PalLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the BG2-Palette-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EBG2PalLabelsContent bg2pallc)
		{
			return base.GetContent(entry, (int)bg2pallc);
		}
	}
}
