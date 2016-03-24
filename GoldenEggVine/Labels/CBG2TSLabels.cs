using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EBG2TSLabelsContent { INDEX=0, HINT=1, DESCRIPTION=2 };

	public class CBG2TSLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EBG2TSLabelsContent)).Cast<EBG2TSLabelsContent>().Max() + 1;
		private static int _entries = 0x20;

		public CBG2TSLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EBG2TSLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the BG2-Tileset-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EBG2TSLabelsContent bg2tslc)
		{
			return base.GetContent(entry, (int)bg2tslc);
		}
	}
}
