using System;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Build.Contributors
{
	/// <summary>
	/// This class abstracts the manifest.json file.
	/// </summary>
	[Serializable]
	public class Contributors
	{
		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private Contributor[] items = Array.Empty<Contributor>();

		public Contributor[] Items => items;
	}
}
