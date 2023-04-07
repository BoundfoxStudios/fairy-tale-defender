using UnityEngine.Splines;

namespace BoundfoxStudios.FairyTaleDefender.Systems.NavigationSystem.PathProviders
{
	public interface ISplineLinkDecisionMaker
	{
		SplineKnotIndex Decide(SplineKnotIndex[] candidates);
	}
}
