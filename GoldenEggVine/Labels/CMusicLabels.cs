using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public enum EMusicLabelsContent { INDEX = 0, HINT = 1, DESCRIPTION = 2 };

	public class CMusicLabels : CLabels
	{
		private static int _columns = (int)Enum.GetValues(typeof(EMusicLabelsContent)).Cast<EMusicLabelsContent>().Max() + 1;
		private static int _entries = 0x10;

		public CMusicLabels(string[] parsecontent) : base(parsecontent, _entries, _columns)
		{
			if (!ValidateIndexes((int)EMusicLabelsContent.INDEX))
			{
				throw new Exception("Error parsing the Music-Labels; Indexer is incorrect!");
			}
		}

		public string GetContent(int entry, EMusicLabelsContent musiclc)
		{
			return base.GetContent(entry, (int)musiclc);
		}
	}
}
