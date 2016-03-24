using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EBG1PalLabelsContent { INDEX = 0, HINT = 1, DESCRIPTION = 2 };

	public class CBG1PalLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EBG1PalLabelsContent)).Cast<EBG1PalLabelsContent>().Max() + 1;
		private static int _entries = 0x20;

		public CBG1PalLabels(string[] parsecontent)
			: base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EBG1PalLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the BG1-Palette-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EBG1PalLabelsContent bg1pallc)
		{
			return base.GetContent(entry, (int)bg1pallc);
		}
	}
}
