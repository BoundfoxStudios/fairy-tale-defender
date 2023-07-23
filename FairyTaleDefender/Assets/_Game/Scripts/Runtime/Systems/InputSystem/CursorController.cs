using System;
using System.Threading;
using BoundfoxStudios.FairyTaleDefender.Common;
using BoundfoxStudios.FairyTaleDefender.Infrastructure.Events.ScriptableObjects;
using BoundfoxStudios.FairyTaleDefender.Systems.InputSystem.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Systems.InputSystem
{
	[AddComponentMenu(Constants.MenuNames.Input + "/" + nameof(CursorController))]
	public class CursorController : MonoBehaviour
	{
		[field: SerializeField]
		private CursorSO Default { get; set; } = default!;

		[field: Header("Listening Channels")]
		[field: SerializeField]
		private BuildableEventChannelSO EnterBuildModeEventChannel { get; set; } = default!;

		[field: SerializeField]
		private VoidEventChannelSO ExitBuildModeEventChannel { get; set; } = default!;

		private CancellationTokenSource? _cancellationTokenSource;

		private void Awake()
		{
			SetCursor(Default);
		}

		private void OnEnable()
		{
			EnterBuildModeEventChannel.Raised += EnterBuildMode;
			ExitBuildModeEventChannel.Raised += ExitBuildMode;
		}

		private void OnDisable()
		{
			EnterBuildModeEventChannel.Raised -= EnterBuildMode;
			ExitBuildModeEventChannel.Raised -= ExitBuildMode;
		}

		private void ExitBuildMode()
		{
			SetCursor(Default);
		}

		private void EnterBuildMode(BuildableEventChannelSO.EventArgs _)
		{
			// SetCursor(Build);
		}

		private void SetCursor(CursorSO cursor)
		{
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource = new();

			AnimateCursorAsync(cursor, _cancellationTokenSource.Token).Forget();
		}

		private async UniTaskVoid AnimateCursorAsync(CursorSO cursor, CancellationToken token)
		{
			var index = 0;
			while (!token.IsCancellationRequested)
			{
				Cursor.SetCursor(cursor.Textures[index], cursor.Hotspot, CursorMode.Auto);

				if (cursor.Textures.Length == 1)
				{
					break;
				}

				index = (index + 1) % cursor.Textures.Length;
				await UniTask.Delay(TimeSpan.FromSeconds(cursor.AnimationDelay), cancellationToken: token);
			}
		}
	}
}
