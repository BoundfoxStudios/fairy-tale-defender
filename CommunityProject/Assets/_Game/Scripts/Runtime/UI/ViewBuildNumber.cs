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

		private async UniTaskVoid Awake()
		{
			_buildNumberText = gameObject.GetComponent<TextMeshProUGUI>();
			_buildNumberText.text = await CreateBuildNumberAsync();
		}

		private async UniTask<string> CreateBuildNumberAsync()
		{
			var _buildManifestReader = new BuildManifestReader();
			var buildManifest = await _buildManifestReader.LoadAsync();
			return $"Build: {Application.version} ({buildManifest.ShortSha})";
		}

		public void CopyBuildNumberToClipboard()
		{
			GUIUtility.systemCopyBuffer = _buildNumberText.text;
		}
	}
}
