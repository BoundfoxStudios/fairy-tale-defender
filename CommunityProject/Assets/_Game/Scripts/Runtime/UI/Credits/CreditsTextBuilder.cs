using System.Linq;
using BoundfoxStudios.CommunityProject.Build.Contributors;
using BoundfoxStudios.CommunityProject.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.CommunityProject.UI.Credits
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(CreditsTextBuilder))]
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class CreditsTextBuilder : MonoBehaviour
	{
		[SerializeField]
		private int CreditTextSize = 75;

		[SerializeField]
		private ScrollRect ScrollView = default!;

		[SerializeField]
		private VerticalLayoutGroup ContentLayoutGroup = default!;

		[SerializeField]
		private string DeveloperPlaceholder = "{{Developer}}";

		[SerializeField]
		private string ArtistPlaceholder = "{{Artist}}";

		[SerializeField]
		private string DocPlaceholder = "{{Doc}}";

		[SerializeField]
		private string IdeaPlaceholder = "{{Idea}}";

		[SerializeField]
		private string AudioPlaceholder = "{{Audio}}";

		private readonly ContributorsReader _contributorsReader = new();
		private TextMeshProUGUI _tmpText = default!;

		[UsedImplicitly]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private async UniTaskVoid Awake()
		{
			_tmpText = gameObject.GetComponentSafe<TextMeshProUGUI>();

			var contributors = await _contributorsReader.LoadAsync();

			BuildCreditsText(contributors);

			var scrollViewRect = ScrollView.GetComponent<RectTransform>().rect;
			var padding = Mathf.FloorToInt(scrollViewRect.height + 1);

			ContentLayoutGroup.padding = new(0, 0, padding, padding);
			ScrollView.verticalNormalizedPosition = 1;
		}

		private void BuildCreditsText(Contributor[] contributors)
		{
			var developerCredits = BuildContributorCredits(contributors, "code");
			var artistCredits = BuildContributorCredits(contributors, "design");
			var documentationCredits = BuildContributorCredits(contributors, "doc");
			var ideasCredits = BuildContributorCredits(contributors, "ideas");
			var soundCredits = BuildContributorCredits(contributors, "audio");

			var credits = _tmpText.text;
			credits = credits
				.Replace(DeveloperPlaceholder, developerCredits)
				.Replace(ArtistPlaceholder, artistCredits)
				.Replace(DocPlaceholder, documentationCredits)
				.Replace(IdeaPlaceholder, ideasCredits)
				.Replace(AudioPlaceholder, soundCredits);
			_tmpText.text = credits;
		}

		private string BuildContributorCredits(Contributor[] contributors, string type)
		{
			var credits = $"<size={CreditTextSize}>";
			credits = contributors
				.Where(contributor => contributor.Contributions.Contains(type))
				.Aggregate(credits, CreateCreditItem);
			credits = $"{credits}</size>";

			return credits;
		}

		private string CreateCreditItem(string original, Contributor contributor)
		{
			var githubLink = contributor.ProfileUrl;
			var displayName = contributor.User;

			var newString = $"{original}<link={githubLink}>{displayName}</link>\n";

			return newString;
		}
	}
}
