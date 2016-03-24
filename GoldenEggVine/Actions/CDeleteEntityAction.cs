using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldenEggVine.Actions
{
	public class CDeleteEntityAction : CAction
	{
		private List<CLevelEntity> _levelentities;

		public CDeleteEntityAction(List<CLevelEntity> levelentities) : base()
		{
			_levelentities = levelentities;
		}

		public override void Do()
		{
			base.Do();
			foreach (CLevelEntity le in _levelentities)
			{
				le.Delete();
			}
		}

		public override void Undo()
		{
			base.Undo();
			foreach (CLevelEntity le in _levelentities)
			{
				le.Create();
			}
		}
	}
}
