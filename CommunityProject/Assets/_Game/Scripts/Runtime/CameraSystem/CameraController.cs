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

		[field: SerializeField]
		private BoxCollider WorldBounds { get; set; } = default!;

		private Vector3 _deltaMovement;
		private Transform _followTarget = default!;

		private void Awake()
		{
			_followTarget = VirtualCamera.Follow;

			AdjustWorldBounds();
		}

		private void AdjustWorldBounds()
		{
			// TODO: we will need to adjust this somehow and calculate it based on the AspectRatio.
			// Right now, it just adjust the collider size, so you can not move the camera too far from the board.
			// We will revisit this once we've some real levels so we know the real size.
			var bounds = WorldBounds.bounds;
			var quarterSize = bounds.size;
			quarterSize /= 4;
			quarterSize.y = bounds.size.y;
			WorldBounds.size = quarterSize;
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
			_deltaMovement.x = delta.x;
			_deltaMovement.z = delta.y;
		}

		private void ReadPanStop()
		{
			_deltaMovement = Vector3.zero;
		}

		private void Update()
		{
			var speed = Settings.Camera.PanSpeed * Time.deltaTime;
			var movement = Quaternion.Euler(0, _followTarget.eulerAngles.y, 0) * _deltaMovement;

			var position = _followTarget.position + movement * speed;
			position = WorldBounds.ClosestPoint(position);
			_followTarget.position = position;
		}
	}
}
