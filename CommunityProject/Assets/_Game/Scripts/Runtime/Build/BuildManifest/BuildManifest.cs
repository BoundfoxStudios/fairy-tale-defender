using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Build.BuildManifest
{
	/// <summary>
	/// This class abstracts the manifest.json file.
	/// </summary>
	[Serializable]
	public class BuildManifest
	{
		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string sha;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string shortSha;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private int runId;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private int runNumber;

		public string Sha => sha;
		public string ShortSha => shortSha;
		public int RunId => runId;
		public int RunNumber => runNumber;

		// I am just a CI trigger and will be removed later. :))
	}
}
