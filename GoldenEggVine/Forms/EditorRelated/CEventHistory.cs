using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.EditorRelated
{
    public class CEventHistory
    {
        private List<Actions.CAction> _actions;
        private int _actionpos;

		public CEventHistory()
		{
			_actions = new List<Actions.CAction>();
			_actionpos = 0;
		}

		public void Do(Actions.CAction action)
		{
			_actions.Add(action);
			action.Do();
			++_actionpos;
		}

		public void Redo()
		{
			if (_actionpos >= _actions.Count)
			{
				throw new Exception("No further actions available!");
			}
			_actions[_actionpos++].Do();
		}

		public void Undo()
		{
			if(_actionpos == 0)
			{
				throw new Exception("No earlier actions available!");
			}
			_actions[--_actionpos].Undo();
		}
    }
}
