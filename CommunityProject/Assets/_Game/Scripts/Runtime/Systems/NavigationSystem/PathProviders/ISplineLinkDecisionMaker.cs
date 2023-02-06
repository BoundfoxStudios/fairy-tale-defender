using UnityEngine.Splines;

namespace BoundfoxStudios.CommunityProject.Systems.NavigationSystem.PathProviders
{
	public interface ISplineLinkDecisionMaker
	{
		SplineKnotIndex Decide(SplineKnotIndex[] candidates);
	}
}
