using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EBG3TSLabelsContent { INDEX=0, HINT=1, DESCRIPTION=2 };

	public class CBG3TSLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EBG3TSLabelsContent)).Cast<EBG3TSLabelsContent>().Max() + 1;
		private static int _entries = 0x40;

		public CBG3TSLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EBG3TSLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the BG3-Tileset-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EBG3TSLabelsContent bg3tslc)
		{
			return base.GetContent(entry, (int)bg3tslc);
		}
	}
}
