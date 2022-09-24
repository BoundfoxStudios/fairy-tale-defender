using BoundfoxStudios.CommunityProject.Build.BuildManifest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace BoundfoxStudios.CommunityProject.UI
{
    [AddComponentMenu(Constants.MenuNames.UI + "/" + nameof(ViewBuildNumber))]
    public class ViewBuildNumber : MonoBehaviour
    {
        private TextMeshProUGUI _buildNumberText;
        private BuildManifestReader bmReader = new BuildManifestReader();

        public BuildManifest unibm;
        private Action LoadBuildManifestAsyncAction;

        private void InitLoadBuildManifestAsyncAction()
        {
            LoadBuildManifestAsyncAction = new Action(async () =>
            {
                unibm = await bmReader.LoadAsync();
                _buildNumberText.text = $"Build: {Application.version} ({unibm.ShortSha})";
            });
        }

        // Start is called before the first frame update
        void Start()
        {
            _buildNumberText = this.gameObject.GetComponent<TextMeshProUGUI>();
            InitLoadBuildManifestAsyncAction();
            LoadBuildManifestAsyncAction.Invoke();
        }
    }
}
