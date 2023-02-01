using BoundfoxStudios.CommunityProject.Infrastructure;
using BoundfoxStudios.CommunityProject.Input.ScriptableObjects;
using BoundfoxStudios.CommunityProject.Settings.ScriptableObjects;
using Cinemachine;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.CameraSystem
{
	[AddComponentMenu(Constants.MenuNames.CameraSystem + "/" + nameof(CameraController))]
	public class CameraController : MonoBehaviour
	{
		[field: SerializeField]
		private CinemachineVirtualCamera VirtualCamera { get; set; } = default!;

		[field: SerializeField]
		private InputReaderSO InputReader { get; set; } = default!;

		[field: SerializeField]
		private SettingsSO Settings { get; set; } = default!;

		private Vector2 _deltaMovement;
		private Transform _followTarget = default!;

		private void Awake()
		{
			Guard.AgainstNull(() => VirtualCamera, this);
			Guard.AgainstNull(() => InputReader, this);
			Guard.AgainstNull(() => Settings, this);

			_followTarget = VirtualCamera.Follow;
		}

		private void OnEnable()
		{
			InputReader.GameplayActions.Pan += ReadPan;
			InputReader.GameplayActions.PanStop += ReadPanStop;
		}

		private void OnDisable()
		{
			InputReader.GameplayActions.Pan -= ReadPan;
			InputReader.GameplayActions.PanStop -= ReadPanStop;
		}

		private void ReadPan(Vector2 delta)
		{
			_deltaMovement = delta;
		}

		private void ReadPanStop()
		{
			_deltaMovement = Vector2.zero;
		}

		private void Update()
		{
			_followTarget.Translate(_deltaMovement * Settings.Camera.PanSpeed * Time.deltaTime);
		}
	}
}
