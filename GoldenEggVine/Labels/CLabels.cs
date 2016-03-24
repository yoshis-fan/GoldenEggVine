using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Labels
{
	public abstract class CLabels
	{
		protected List<string[]> _attribs = new List<string[]>();
		protected int _specificentries;
		protected int _specificcolumns;

		public CLabels(string[] filecontent, int entries, int columns)
		{
			_specificentries = entries;
			_specificcolumns = columns;
			foreach (string s in filecontent)
			{
				string[] values = CLineProcessor.ProcessLine(s);
				if(values.Length != columns)
				{
					throw new Exception("File may only contain " + columns + " columns per line, but this column contains " + values.Length + " columns");
				}
				else
				{
					_attribs.Add(values);
				}
			}
			if(_attribs.Count != entries)
			{
				throw new Exception("Number of entries must be " + entries + ", but there are " + _attribs.Count + " entries in the list!");
			}
		}

		public string[] GetRow(int entry)
		{
			return _attribs[entry];
		}

		public int Count()
		{
			return _attribs.Count;
		}

		public string GetContent(int entry, int column)
		{
			return _attribs[entry][column];
		}

		protected bool ValidateIndexes(int index)
		{
			for (int i = 0; i < _attribs.Count; i++)
			{
				int compare = -1;
				Int32.TryParse(_attribs[i][index], System.Globalization.NumberStyles.AllowHexSpecifier, System.Globalization.CultureInfo.InvariantCulture, out compare);
				if (compare != i)
				{
					return false;
				}
			}
			return true;
		}
	}
}
