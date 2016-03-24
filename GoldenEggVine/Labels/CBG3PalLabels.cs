using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EBG3PalLabelsContent { INDEX = 0, HINT = 1, DESCRIPTION = 2 };

	public class CBG3PalLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EBG3PalLabelsContent)).Cast<EBG3PalLabelsContent>().Max() + 1;
		private static int _entries = 0x40;

		public CBG3PalLabels(string[] parsecontent)
			: base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EBG3PalLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the BG3-Palette-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EBG3PalLabelsContent bg3pallc)
		{
			return base.GetContent(entry, (int)bg3pallc);
		}
	}
}
