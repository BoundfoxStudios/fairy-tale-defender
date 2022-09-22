using BoundfoxStudios.CommunityProject.Build.BuildManifest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace BoundfoxStudios.CommunityProject
{
    public class ViewBuildNumber : MonoBehaviour
    {
        private TextMeshProUGUI BuildNumberText;
        private BuildManifestReader bmReader = new BuildManifestReader();

        public BuildManifest unibm;
        private Action LoadBuildManifestAsyncAction;

        private void InitLoadBuildManifestAsyncAction()
        {
            LoadBuildManifestAsyncAction = new Action(async () =>
            {
                unibm = await bmReader.LoadAsync();
                BuildNumberText.text = string.Format("Build: {0} ({1})", Application.version, unibm.ShortSha);
            });
        }

        // Start is called before the first frame update
        void Start()
        {
            BuildNumberText = this.gameObject.GetComponent<TextMeshProUGUI>();
            InitLoadBuildManifestAsyncAction();
            LoadBuildManifestAsyncAction.Invoke();
        }
    }
}
