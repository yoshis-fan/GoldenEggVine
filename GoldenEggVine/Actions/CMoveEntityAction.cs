using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Actions
{
	public class CMoveEntityAction : CAction
	{
		private List<CLevelEntity> _levelentities;
		private SVector _delta;

		public CMoveEntityAction(List<CLevelEntity> levelentities, SVector delta)
		{
			_levelentities = levelentities;
			_delta = delta;
		}

		public override void Do()
		{
			base.Do();
			foreach (CLevelEntity le in _levelentities)
			{
				le.SetPosition(le.GetPosition() + _delta);
			}
		}

		public override void Undo()
		{
			base.Undo();
			foreach(CLevelEntity le in _levelentities)
			{
				le.SetPosition(le.GetPosition() - _delta);
			}
		}


	}
}
