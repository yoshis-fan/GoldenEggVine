using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Actions
{
	public class CCreateEntityAction : CAction
	{
		private List<CLevelEntity> _levelentities;

		public CCreateEntityAction(List<CLevelEntity> levelentities)
		{
			_levelentities = levelentities;
		}

		public override void Do()
		{
			base.Do();
			foreach (CLevelEntity le in _levelentities)
			{
				le.Create();
			}
		}

		public override void Undo()
		{
			base.Undo();
			foreach (CLevelEntity le in _levelentities)
			{
				le.Delete();
			}
		}

	}
}
