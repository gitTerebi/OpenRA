#region Copyright & License Information
/*
 * Copyright 2007-2015 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using OpenRA.Traits;

namespace OpenRA.Mods.Common.Traits
{
	[Desc("Changes the animation when the actor constructed a building.")]
	public class WithBuildingPlacedAnimationInfo : ITraitInfo, Requires<RenderSimpleInfo>
	{
		[Desc("Sequence name to use")]
		[SequenceReference] public readonly string Sequence = "build";

		public object Create(ActorInitializer init) { return new WithBuildingPlacedAnimation(init.Self, this); }
	}

	public class WithBuildingPlacedAnimation : INotifyBuildingPlaced, INotifyBuildComplete
	{
		readonly WithBuildingPlacedAnimationInfo info;
		readonly RenderSimple renderSimple;
		bool buildComplete;

		public WithBuildingPlacedAnimation(Actor self, WithBuildingPlacedAnimationInfo info)
		{
			this.info = info;
			renderSimple = self.Trait<RenderSimple>();
			buildComplete = !self.HasTrait<Building>();
		}

		public void BuildingComplete(Actor self)
		{
			buildComplete = true;
		}

		public void BuildingPlaced(Actor self)
		{
			if (buildComplete)
				renderSimple.PlayCustomAnim(self, info.Sequence);
		}
	}
}