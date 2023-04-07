using UnityEngine;
using UnityEngine.Splines;

namespace BoundfoxStudios.CommunityProject.Systems.NavigationSystem.PathProviders
{
	public class RandomSplineLinkDecisionMaker : ISplineLinkDecisionMaker
	{
		public SplineKnotIndex Decide(SplineKnotIndex[] candidates) => candidates[Random.Range(0, candidates.Length)];
	}
}
