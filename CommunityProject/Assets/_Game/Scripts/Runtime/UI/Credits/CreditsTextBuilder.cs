using System.Linq;
using BoundfoxStudios.CommunityProject.Build.Contributors;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoundfoxStudios.CommunityProject.UI.Credits
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class CreditsTextBuilder : MonoBehaviour
	{
		private TextMeshProUGUI _tmpText;
		private ContributorsReader _contributorsReader;

		[SerializeField]
		private int CreditTextSize = 75;

		[SerializeField]
		private ScrollRect ScrollView;

		[SerializeField]
		private VerticalLayoutGroup ContentLayoutGroup;

		// Start is called before the first frame update
		void Start()
		{

		}

		private async UniTaskVoid Awake()
		{
			_tmpText = GetComponent<TextMeshProUGUI>();
			_contributorsReader = new ContributorsReader();

			var contributors = await _contributorsReader.LoadAsync();

			BuildCreditsText(contributors);

			var scrollViewRect = ScrollView.GetComponent<RectTransform>().rect;
			var padding = Mathf.FloorToInt(scrollViewRect.height + 1);

			ContentLayoutGroup.padding = new(0, 0, padding, padding);
			ScrollView.verticalNormalizedPosition = 1;
		}


		private void BuildCreditsText(Contributor[] contributors)
		{
			var developerCredits = BuildDeveloperCredits(contributors);
			var artistCredits = BuildArtistsCredits(contributors);
			var documentationCredits = BuildDocumentationCredits(contributors);
			var ideasCredits = BuildIdeasCredits(contributors);
			var soundCredits = BuildSoundCredits(contributors);

			_tmpText.text = _tmpText.text.Replace("{{Developer}}", developerCredits);
			_tmpText.text = _tmpText.text.Replace("{{Artist}}", artistCredits);
			_tmpText.text = _tmpText.text.Replace("{{Doc}}", documentationCredits);
			_tmpText.text = _tmpText.text.Replace("{{Idea}}", ideasCredits);
			_tmpText.text = _tmpText.text.Replace("{{Audio}}", soundCredits);
		}

		private string BuildDeveloperCredits(Contributor[] contributors)
		{
			string credits = $"<size={CreditTextSize}>";
			foreach (var contributor in contributors)
			{
				if (contributor.Contributions.Contains("code"))
				{
					credits = AddNextCreditItem(credits, contributor);
				}
			}

			credits = $"{credits}</size>";

			return credits;
		}

		private string BuildArtistsCredits(Contributor[] contributors)
		{
			string credits = $"<size={CreditTextSize}>";
			foreach (var contributor in contributors)
			{
				if (contributor.Contributions.Contains("design"))
				{
					credits = AddNextCreditItem(credits, contributor);
				}
			}

			credits = $"{credits}</size>";

			return credits;
		}

		private string BuildDocumentationCredits(Contributor[] contributors)
		{
			string credits = $"<size={CreditTextSize}>";
			foreach (var contributor in contributors)
			{
				if (contributor.Contributions.Contains("doc"))
				{
					credits = AddNextCreditItem(credits, contributor);
				}
			}

			credits = $"{credits}</size>";

			return credits;
		}

		private string BuildIdeasCredits(Contributor[] contributors)
		{
			string credits = $"<size={CreditTextSize}>";
			foreach (var contributor in contributors)
			{
				if (contributor.Contributions.Contains("ideas"))
				{
					credits = AddNextCreditItem(credits, contributor);
				}
			}

			credits = $"{credits}</size>";

			return credits;
		}

		private string BuildSoundCredits(Contributor[] contributors)
		{
			string credits = $"<size={CreditTextSize}>";
			foreach (var contributor in contributors)
			{
				if (contributor.Contributions.Contains("audio"))
				{
					credits = AddNextCreditItem(credits, contributor);
				}
			}

			credits = $"{credits}</size>";

			return credits;
		}

		private string AddNextCreditItem(string original, Contributor contributor)
		{
			var githubLink = contributor.ProfileUrl;
			var displayName = contributor.User;

			var newString = $"{original}<link={githubLink}>{displayName}</link>\n";

			return newString;
		}
	}
}
