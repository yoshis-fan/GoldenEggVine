using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum ELevelLabelsContent { INDEX = 0, DESCRIPTION = 1 };

	public class CLevelLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(ELevelLabelsContent)).Cast<ELevelLabelsContent>().Max() + 1;
		private static int _entries = 0xDE;

		public CLevelLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)ELevelLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the Level-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, ELevelLabelsContent llc)
		{
			return base.GetContent(entry, (int)llc);
		}
	}
}