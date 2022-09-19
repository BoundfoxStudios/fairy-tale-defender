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
		private string url;

		[SerializeField]
		// ReSharper disable once InconsistentNaming
		private int contributions;

		public string User => user;
		public string Url => url;
		public int Contributions => contributions;
	}
}
