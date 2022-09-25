using BoundfoxStudios.CommunityProject.Build.BuildManifest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Cysharp.Threading.Tasks;

namespace BoundfoxStudios.CommunityProject.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ViewBuildNumber))]
	[RequireComponent(typeof(TextMeshProUGUI))]

	public class ViewBuildNumber : MonoBehaviour
	{
		private TextMeshProUGUI _buildNumberText;
		private BuildManifestReader _buildManifestReader = new BuildManifestReader();

		private async UniTask CreateBuildNumber()
		{
			var buildManifest = await _buildManifestReader.LoadAsync();
			_buildNumberText.text = $"Build: {Application.version} ({buildManifest.ShortSha})";
		}

		// Start is called before the first frame update
		private async UniTaskVoid Awake()
		{
			_buildNumberText = gameObject.GetComponent<TextMeshProUGUI>();

			var buildManifest = await _buildManifestReader.LoadAsync();

			_buildNumberText.text = $"Build: {Application.version} ({buildManifest.ShortSha})";
		}

		// Kopiert die Buildnummer in die Windows Zwischenablage
		public void CopyBuildNumberToClipboard()
		{
			GUIUtility.systemCopyBuffer = _buildNumberText.text;
		}
	}
}
