using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Actions
{
    public abstract class CAction
    {
        /*
         * =================
         * = ACTION TYPES: =
         * =================
         * 
         * MOVE OBJECTSET
         * ALTER SIZE OF OBJECTSET
         * DELETE OBJECTSET
         * CREATE/PASTE OBJECTSET
         * EDIT SCREEN EXIT
         * HEADER ALTERED
         * PALETTE ALTERED
         */

		private bool _executed;

		public CAction() { _executed = false; }

        //Performs the CounterAction to that Action
		/// <summary>
		/// Do not call without EventHistory-class!
		/// </summary>
		public virtual void Undo() { _executed = false; }

        //Performs that Action again
		/// <summary>
		/// Do not call without EventHistory-class!
		/// </summary>
		public virtual void Do() { _executed = true; }

    }
}
