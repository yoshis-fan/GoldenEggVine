using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EMiniBattleLabelsContent { INDEX = 0, DESCRIPTION = 1 };

	public class CMiniBattleLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EMiniBattleLabelsContent)).Cast<EMiniBattleLabelsContent>().Max() + 1;
		private static int _entries = 0xA;

		public CMiniBattleLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EMiniBattleLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the Exit-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EMiniBattleLabelsContent minibattlelc)
		{
			return base.GetContent(entry, (int)minibattlelc);
		}
	}
}
