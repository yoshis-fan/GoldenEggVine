using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum ELevelModeLabelsContent { INDEX=0, DESCRIPTION=1 };

	public class CLevelModeLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(ELevelModeLabelsContent)).Cast<ELevelModeLabelsContent>().Max() + 1;
		private static int _entries = 0x10;

		public CLevelModeLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)ELevelModeLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the Level-Mode-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, ELevelModeLabelsContent lmlc)
		{
			return base.GetContent(entry, (int)lmlc);
		}

	}
}
