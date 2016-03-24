using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EExitLabelsContent { INDEX = 0, DESCRIPTION = 1 };

	public class CExitLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EExitLabelsContent)).Cast<EExitLabelsContent>().Max() + 1;
		private static int _entries = 0xB;

		public CExitLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EExitLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the Exit-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EExitLabelsContent exitlc)
		{
			return base.GetContent(entry, (int)exitlc);
		}
	}
}
