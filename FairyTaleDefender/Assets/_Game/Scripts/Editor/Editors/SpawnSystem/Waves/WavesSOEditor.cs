using System.Linq;
using BoundfoxStudios.FairyTaleDefender.Editor.Extensions;
using BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves;
using BoundfoxStudios.FairyTaleDefender.Systems.SpawnSystem.Waves.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Editors.SpawnSystem.Waves
{
	[CustomEditor(typeof(WavesSO))]
	public class WavesSOEditor : UnityEditor.Editor
	{
		private SerializedProperty _wavesProperty = default!;

		private void OnEnable()
		{
			_wavesProperty = serializedObject.FindRealProperty(nameof(WavesSO.Waves));
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var waves = (WavesSO)target;

			RenderAddWaveButtons();
			RenderTotalTime(waves);
			RenderWaveValidation(waves);
		}

		private static void RenderWaveValidation(WavesSO waves)
		{
			var isAnyWaveInvalid = waves.Waves.Any(wave => !wave.IsValid);

			if (isAnyWaveInvalid)
			{
				EditorGUILayout.HelpBox("At least one wave is not valid.", MessageType.Error);
			}
		}

		private static void RenderTotalTime(WavesSO waves)
		{
			var totalTimeForAllWaves = waves.Waves.Sum(wave => wave.TimeToSpawnAllEnemies);

			EditorGUILayout.SelectableLabel($"Total time to spawn all enemies: {totalTimeForAllWaves:0.00} s");
		}

		private void RenderAddWaveButtons()
		{
			EditorGUILayout.HelpBox("Do NOT use the plus button to add a wave, but use the buttons below!", MessageType.Info);

			if (GUILayout.Button("Add Single Enemy Type Wave"))
			{
				AddWave<SingleEnemyTypeWave>();
			}
		}

		private void AddWave<T>()
			where T : Wave, new()
		{
			var index = _wavesProperty.arraySize;
			_wavesProperty.InsertArrayElementAtIndex(index);

			var waveProperty = _wavesProperty.GetArrayElementAtIndex(index);
			waveProperty.managedReferenceValue = new T();
			serializedObject.ApplyModifiedProperties();
		}
	}
}
