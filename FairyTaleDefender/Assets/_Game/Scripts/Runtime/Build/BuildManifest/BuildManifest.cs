using System;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Build.BuildManifest
{
	/// <summary>
	/// This class abstracts the manifest.json file.
	/// </summary>
	[Serializable]
	public class BuildManifest
	{
		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private string sha = string.Empty;

		public string Sha => sha;
		public string ShortSha => Sha[..8];
	}
}
