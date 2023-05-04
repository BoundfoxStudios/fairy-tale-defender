using System;
using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Build.Contributors;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.UI.Credits.ScriptableObjects;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.FairyTaleDefender.UI.Credits
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(CreditsTextBuilder))]
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class CreditsTextBuilder : MonoBehaviour
	{
		[field: SerializeField]
		private CreditSO[] AdditionalCredits { get; set; } = Array.Empty<CreditSO>();

		[field: SerializeField]
		private int CreditTextSize { get; set; } = 150;

		[field: SerializeField]
		private ScrollRect ScrollView { get; set; } = default!;

		[field: SerializeField]
		private VerticalLayoutGroup ContentLayoutGroup { get; set; } = default!;

		[field: SerializeField]
		private string DeveloperPlaceholder { get; set; } = "{{Developer}}";

		[field: SerializeField]
		private string ArtistPlaceholder { get; set; } = "{{Artist}}";

		[field: SerializeField]
		private string DocPlaceholder { get; set; } = "{{Doc}}";

		[field: SerializeField]
		private string IdeaPlaceholder { get; set; } = "{{Idea}}";

		[field: SerializeField]
		private string AudioPlaceholder { get; set; } = "{{Audio}}";

		private readonly ContributorsReader _contributorsReader = new();
		private TextMeshProUGUI _tmpText = default!;

		[UsedImplicitly]
		// ReSharper disable once Unity.IncorrectMethodSignature
		private async UniTaskVoid Awake()
		{
			_tmpText = GetComponent<TextMeshProUGUI>();

			var contributors = await _contributorsReader.LoadAsync();

			BuildCreditsText(contributors.Concat(AdditionalCredits.Select(credit => credit.Contributor)).ToArray());

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
			var credits = $"<size={CreditTextSize}%>";
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

			var result = $"{original}";

			if (string.IsNullOrWhiteSpace(contributor.GitHubAccount))
			{
				result += displayName;
			}
			else
			{
				result += $"<link={githubLink}>{displayName}</link>";
			}

			result += "\n";

			return result;
		}
	}
}
