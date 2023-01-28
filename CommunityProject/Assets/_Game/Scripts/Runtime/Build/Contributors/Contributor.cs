using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Build.Contributors
{
	[Serializable]
	public class Contributor
	{
		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string user;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string githubAccount;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string profileUrl;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string[] contributions;

		/// <summary>
		/// Username as set in the GitHub profile information
		/// </summary>
		public string User => user;

		/// <summary>
		/// The contributor's GitHub account name
		/// </summary>
		public string GitHubAccount => githubAccount;

		/// <summary>
		/// Full URL to the contributors GitHub profile
		/// </summary>
		public string ProfileUrl => $"https://github.com/{GitHubAccount}";

		/// <summary>
		/// List with contribution types.
		/// Item will be a string of the AllContributors bot: https://allcontributors.org/docs/en/emoji-key
		/// e.g. "audio", "code", "doc", ...
		/// </summary>
		public string[] Contributions => contributions;
	}
}
