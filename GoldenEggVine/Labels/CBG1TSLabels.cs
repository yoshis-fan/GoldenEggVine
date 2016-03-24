using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EBG1TSLabelsContent { INDEX = 0, HINT = 1, DESCRIPTION=2 };

	public class CBG1TSLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EBG1TSLabelsContent)).Cast<EBG1TSLabelsContent>().Max() + 1;
		private static int _entries = 0x10;

		public CBG1TSLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EBG1TSLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the BG1-Tileset-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EBG1TSLabelsContent bg1tslc)
		{
			return base.GetContent(entry, (int)bg1tslc);
		}
	}
}
