using BoundfoxStudios.CommunityProject.Build.BuildManifest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace BoundfoxStudios.CommunityProject.UI
{
	[AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ViewBuildNumber))]
	[RequireComponent(typeof(TextMeshProUGUI))]

	public class ViewBuildNumber : MonoBehaviour
	{
		private TextMeshProUGUI _buildNumberText;
		private BuildManifestReader _buildManifestReader = new BuildManifestReader();

		private BuildManifest _buildManifest;
		private Action LoadBuildManifestAsyncAction;

		private void InitLoadBuildManifestAsyncAction()
		{
			LoadBuildManifestAsyncAction = new Action(async () =>
			{
				_buildManifest = await _buildManifestReader.LoadAsync();
				_buildNumberText.text = $"Build: {Application.version} ({_buildManifest.ShortSha})";
			});
		}

		// Start is called before the first frame update
		void Awake()
		{
			_buildNumberText = gameObject.GetComponent<TextMeshProUGUI>();
			InitLoadBuildManifestAsyncAction();
			LoadBuildManifestAsyncAction.Invoke();
		}

		// Kopiert die Buildnummer in die Windows Zwischenablage
		public void CopyBuildNumberToClipboard()
		{
			GUIUtility.systemCopyBuffer = _buildNumberText.text;
		}
	}
}
