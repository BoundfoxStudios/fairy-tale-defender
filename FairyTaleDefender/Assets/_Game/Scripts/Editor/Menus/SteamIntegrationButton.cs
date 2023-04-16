using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Menus
{
	[InitializeOnLoad]
	public static class SteamIntegrationButton
	{
		private static ScriptableObject? _currentToolbar;
		private static int _lastInstanceID;
		private static readonly Type ToolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
		private static VisualElement? _parentRight;

		static SteamIntegrationButton()
		{
			EditorApplication.update -= Update;
			EditorApplication.update += Update;
		}

		private static void Update()
		{
			EnsureToolbar();
			ResetCachedToolbarIfNeeded();
			CreateAnchorZones();
		}

		private static void CreateAnchorZones()
		{
			if (_currentToolbar is null || _parentRight is not null)
			{
				return;
			}

			var root = _currentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
			var rawRoot = root?.GetValue(_currentToolbar);

			if (rawRoot is null)
			{
				return;
			}

			var visualElementRoot = rawRoot as VisualElement;

			AttachToolbarRight(visualElementRoot.Q("ToolbarZoneRightAlign"));
		}

		private static void AttachToolbarRight(VisualElement root)
		{
			_parentRight?.RemoveFromHierarchy();

			_parentRight = null;
			_parentRight = new() { style = { flexGrow = 1, flexDirection = FlexDirection.Row, marginRight = 2 } };
			_parentRight.Add(new() { style = { flexGrow = 1 } });

			var isSteamIntegrationEnabled = IsSteamIntegrationEnabled(out _);
			_parentRight.Add(CreateToolbarIcon("EditorCustomization/steam_icon_32x32.png",
				$"Steam Integration is {(isSteamIntegrationEnabled ? "enabled" : "disabled")}",
				isSteamIntegrationEnabled
					? Color.green
					: Color.red,
				ToggleSteamIntegration)
			);

			root.Add(_parentRight);
		}

		private static bool IsSteamIntegrationEnabled(out HashSet<string> uniqueDefines)
		{
			PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				out var defines);

			uniqueDefines = new(defines);

			return uniqueDefines.Contains(Constants.CompilerDirectives.EnableSteam);
		}

		private static void ToggleSteamIntegration()
		{
			var isSteamIntegrationEnabled = IsSteamIntegrationEnabled(out var uniqueDefines);

			if (isSteamIntegrationEnabled)
			{
				uniqueDefines.Remove(Constants.CompilerDirectives.EnableSteam);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
					uniqueDefines.ToArray());

				return;
			}

			uniqueDefines.Add(Constants.CompilerDirectives.EnableSteam);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
				uniqueDefines.ToArray());
		}

		private static VisualElement CreateToolbarIcon(string iconName, string tooltip, Color? iconTint, Action onClick)
		{
			Button button = new(onClick) { tooltip = tooltip };
			FitToolbarIconChildrenStyle(button);

			VisualElement icon = new();
			icon.AddToClassList("unity-editor-toolbar-element__icon");
			icon.style.backgroundImage = Background.FromTexture2D(Addressables.LoadAssetAsync<Texture2D>(iconName).Result);

			if (iconTint is { } tint)
			{
				icon.style.unityBackgroundImageTintColor = new(tint);
			}

			button.Add(icon);

			return button;
		}

		private static void FitToolbarIconChildrenStyle(VisualElement element)
		{
			element.AddToClassList("unity-toolbar-button");
			element.AddToClassList("unity-editor-toolbar-element");
			element.RemoveFromClassList("unity-button");
			element.style.marginRight = 0;
			element.style.marginLeft = 0;
		}

		private static void ResetCachedToolbarIfNeeded()
		{
			if (_currentToolbar is null || _parentRight is null || _currentToolbar.GetInstanceID() == _lastInstanceID)
			{
				return;
			}

			_parentRight.RemoveFromHierarchy();
			_parentRight = null;

			_lastInstanceID = _currentToolbar.GetInstanceID();
		}

		private static void EnsureToolbar()
		{
			if (_currentToolbar is not null)
			{
				return;
			}

			var toolbars = Resources.FindObjectsOfTypeAll(ToolbarType);
			_currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
		}
	}
}
