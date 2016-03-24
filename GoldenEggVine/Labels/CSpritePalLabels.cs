using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum ESprPalLabelsContent { INDEX=0, HINT=1, DESCRIPTION=2 };

	public class CSprPalLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(ESprPalLabelsContent)).Cast<ESprPalLabelsContent>().Max() + 1;
		private static int _entries = 0x10;

		public CSprPalLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if(!ValidateIndexes((int)ESprPalLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the Sprite-Palette-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, ESprPalLabelsContent sprpallc)
		{
			return base.GetContent(entry, (int)sprpallc);
		}
	}
}
